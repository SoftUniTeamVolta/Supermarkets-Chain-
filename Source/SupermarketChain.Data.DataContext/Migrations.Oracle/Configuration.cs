namespace SupermarketChain.Data.DataContext.Migrations.Oracle
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Models.OracleXEModels;
    using System;

    public sealed class Configuration : DbMigrationsConfiguration<OracleDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"Migrations.Oracle";
            //ContextKey = "SupermarketChain.Data.DataContext.OracleDataContext";
        }

        protected override void Seed(OracleDataContext context)
        {
            if (!context.Measures.Any())
            {
                Configuration.AddMeasureTestData(context);
            }

            if (!context.Vendors.Any())
            {
                Configuration.AddVendorTestData(context);
            }

            if (!context.Products.Any())
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
                new VENDOR {Name = "Zagorka SA"},
                new VENDOR {Name = "Kamenitza SA"},
                new VENDOR {Name = "Bio Bulgaria Ltd."},
                new VENDOR {Name = "Mondelez Bulgaria Ltd."},
                new VENDOR {Name = "Intersnack Bulgaria Ltd."}
            };

            vendors.ForEach(v => context.Vendors.Add(v));
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
                new MEASURE {Name = "milliliter", Abbreviation = "ml"},
                new MEASURE {Name = "liter", Abbreviation = "l"},
                new MEASURE {Name = "gram", Abbreviation = "gr"},
                new MEASURE {Name = "kilogram", Abbreviation = "kg"},
                new MEASURE {Name = "piece", Abbreviation = "pc"}
            };

            measures.ForEach(m => context.Measures.Add(m));
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
            var zagorkaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Zagorka SA");
            var kamentzaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Kamenitza SA");
            var bioBulgariaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Bio Bulgaria Ltd.");
            var mondelezVendor = context.Vendors.FirstOrDefault(v => v.Name == "Mondelez Bulgaria Ltd.");
            var intersnackVendor = context.Vendors.FirstOrDefault(v => v.Name == "Intersnack Bulgaria Ltd.");
            
            
            var measureL = context.Measures.FirstOrDefault(m => m.Name == "liter");
            var measureML = context.Measures.FirstOrDefault(m => m.Name == "milliliter");
            var measureGR = context.Measures.FirstOrDefault(m => m.Name == "gram");
            var measureKG = context.Measures.FirstOrDefault(m => m.Name == "kilogram");
            var measureP = context.Measures.FirstOrDefault(m => m.Name == "piece");

            var products = new List<PRODUCT>
            {
                new PRODUCT {VendorId = zagorkaVendor.Id, Name = "Beer \"Zagorka\"", MeasureId = measureL.Id, Price = 1.19m, CreatedOn = new DateTime(2015, 1, 1)},
                new PRODUCT {VendorId = kamentzaVendor.Id, Name = "Beer \"Kamenitza\"", MeasureId = measureL.Id, Price = 1.09m},
                new PRODUCT {VendorId = bioBulgariaVendor.Id, Name = "\"Whole rye boza\" Harmonica Boza", MeasureId = measureML.Id, Price = 1.42m, PreserveCreatedOn = false}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}