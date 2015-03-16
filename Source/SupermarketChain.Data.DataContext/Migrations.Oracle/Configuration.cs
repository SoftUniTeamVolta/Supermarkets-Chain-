namespace SupermarketChain.Data.DataContext.Migrations.Oracle
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models.OracleXEModels;

    public sealed class Configuration : DbMigrationsConfiguration<OracleDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations.Oracle";
            ContextKey = "SupermarketChain.Data.DataContext.OracleDataContext";
        }

        protected override void Seed(OracleDataContext context)
        {
            if (!context.MEASURES.Any())
            {
                Configuration.AddMeasureTestData(context);
            }

            if (!context.VENDORS.Any())
            {
                Configuration.AddVendorTestData(context);
            }

            if (!context.PRODUCTS.Any())
            {
                Configuration.AddProductTestData(context);
            }
        }

        private static void AddVendorTestData(OracleDataContext context)
        {
            string createSequenceVendorTable =
                @"CREATE SEQUENCE VEND_SEQ MINVALUE 10 MAXVALUE 9999999999999999999999999999 INCREMENT BY 10;
create or replace TRIGGER VEND_BIR
BEFORE INSERT ON VENDORS
FOR EACH ROW
BEGIN
SELECT VEND_SEQ.NEXTVAL
INTO :new.id
FROM dual;
END;
";

            context.Database.SqlQuery<VENDOR>(createSequenceVendorTable);
            var vendors = new List<VENDOR>
            {
                new VENDOR {NAME = "Zagorka SA"},
                new VENDOR {NAME = "Kamenitza SA"},
                new VENDOR {NAME = "Bio Bulgaria Ltd."},
                new VENDOR {NAME = "Mondelez Bulgaria Ltd."},
                new VENDOR {NAME = "Intersnack Bulgaria Ltd."}
            };

            vendors.ForEach(v => context.VENDORS.Add(v));
            context.SaveChanges();
        }

        private static void AddMeasureTestData(OracleDataContext context)
        {
            string createSequenceMeasureTable = @"CREATE SEQUENCE MES_SEQ MINVALUE 100 MAXVALUE 9999999999999999999999999999 INCREMENT BY 100;
create or replace TRIGGER MES_BIR
BEFORE INSERT ON MEASURES
FOR EACH ROW
BEGIN
SELECT MES_SEQ.NEXTVAL
INTO :new.id
FROM dual;
END;";

            context.Database.SqlQuery<MEASURE>(createSequenceMeasureTable);
            var measures = new List<MEASURE>
            {
                new MEASURE {NAME = "milliliter", ABBREVIATION = "ml"},
                new MEASURE {NAME = "liter", ABBREVIATION = "l"},
                new MEASURE {NAME = "gram", ABBREVIATION = "gr"},
                new MEASURE {NAME = "kilogram", ABBREVIATION = "kg"},
                new MEASURE {NAME = "piece", ABBREVIATION = "pc"}
            };

            measures.ForEach(m => context.MEASURES.Add(m));
            context.SaveChanges();
        }

        private static void AddProductTestData(OracleDataContext context)
        {
            string createSequenceProductTable = @"CREATE SEQUENCE PROD_SEQ;
CREATE OR REPLACE TRIGGER PROD_BIR
BEFORE INSERT ON PRODUCTS
FOR EACH ROW
BEGIN
SELECT PROD_SEQ.NEXTVAL
INTO :new.id
FROM dual;
END;";
            context.Database.SqlQuery<PRODUCT>(createSequenceProductTable);
            var vendor = context.VENDORS.FirstOrDefault(v => v.NAME == "Zagorka SA");
            var measure = context.MEASURES.FirstOrDefault(m => m.NAME == "liter");
            var products = new List<PRODUCT>
            {
                new PRODUCT {VENDOR_ID = vendor.ID, NAME = "Beer \"Zagorka\"", MEASURE_ID = measure.ID}
            };

            products.ForEach(p => context.PRODUCTS.Add(p));
            context.SaveChanges();
        }
    }
}