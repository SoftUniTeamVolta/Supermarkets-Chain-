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

            context.Database.SqlQuery<VENDORS>(createSequenceVendorTable);
            var vendors = new List<VENDORS>
            {
                new VENDORS {NAME = "Zagorka SA"},
                new VENDORS {NAME = "Kamenitza SA"},
                new VENDORS {NAME = "Bio Bulgaria Ltd."},
                new VENDORS {NAME = "Mondelez Bulgaria Ltd."},
                new VENDORS {NAME = "Intersnack Bulgaria Ltd."}
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

            context.Database.SqlQuery<MEASURES>(createSequenceMeasureTable);
            var measures = new List<MEASURES>
            {
                new MEASURES {NAME = "milliliter", ABBREVIATION = "ml"},
                new MEASURES {NAME = "liter", ABBREVIATION = "l"},
                new MEASURES {NAME = "gram", ABBREVIATION = "gr"},
                new MEASURES {NAME = "kilogram", ABBREVIATION = "kg"},
                new MEASURES {NAME = "piece", ABBREVIATION = "pc"}
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
            context.Database.SqlQuery<PRODUCTS>(createSequenceProductTable);
            var vendor = context.VENDORS.FirstOrDefault(v => v.NAME == "Zagorka SA");
            var measure = context.MEASURES.FirstOrDefault(m => m.NAME == "liter");
            var products = new List<PRODUCTS>
            {
                new PRODUCTS {VENDOR_ID = vendor.ID, NAME = "Beer \"Zagorka\"", MEASURE_ID = measure.ID}
            };

            products.ForEach(p => context.PRODUCTS.Add(p));
            context.SaveChanges();
        }
    }
}