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
                new VENDOR {Name = "Intersnack Bulgaria Ltd."},
                new VENDOR {Name = "Bolyarka VT SA"},
                new VENDOR {Name = "Detelina's Nuts"},
                new VENDOR {Name = "\"Sun Moon\" Bakery"},
                new VENDOR {Name = "Sani-Kons Todorovi Sie SD"},
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
            var bolyarkaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Bolyarka VT SA");
            var detelinaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Detelina's Nuts");
            var sunMoonVendor = context.Vendors.FirstOrDefault(v => v.Name == "\"Sun Moon\" Bakery");
            var saniKonsTodoroviVendor = context.Vendors.FirstOrDefault(v => v.Name == "Sani-Kons Todorovi Sie SD");
            
            
            var measureL = context.Measures.FirstOrDefault(m => m.Name == "liter");
            var measureML = context.Measures.FirstOrDefault(m => m.Name == "milliliter");
            var measureGR = context.Measures.FirstOrDefault(m => m.Name == "gram");
            var measureKG = context.Measures.FirstOrDefault(m => m.Name == "kilogram");
            var measureP = context.Measures.FirstOrDefault(m => m.Name == "piece");

            var products = new List<PRODUCT>
            {
                new PRODUCT {VendorId = zagorkaVendor.Id, Name = "Beer \"Zagorka\"", MeasureId = measureL.Id, Price = 1.99m, CreatedOn = new DateTime(2015, 1, 1)},
                new PRODUCT {VendorId = zagorkaVendor.Id, Name = "Beer \"Ariana\"", MeasureId = measureL.Id, Price = 1.69m, CreatedOn = new DateTime(2015, 2, 25)},
                new PRODUCT {VendorId = zagorkaVendor.Id, Name = "Beer \"Starobrno\"", MeasureId = measureML.Id, Price = 2.22m},

                new PRODUCT {VendorId = kamentzaVendor.Id, Name = "Beer \"Kamenitza\"", MeasureId = measureL.Id, Price = 1.72m},
                new PRODUCT {VendorId = kamentzaVendor.Id, Name = "Beer \"Staropramen\"", MeasureId = measureML.Id, Price = 2.66m},
                new PRODUCT {VendorId = kamentzaVendor.Id, Name = "Beer \"Burgasko\"", MeasureId = measureML.Id, Price = 1.7m, PreserveCreatedOn = false},
                new PRODUCT {VendorId = kamentzaVendor.Id, Name = "Beer \"Pirinsko\"", MeasureId = measureML.Id, Price = 1.7m},
                
                new PRODUCT {VendorId = bioBulgariaVendor.Id, Name = "\"Whole rye boza\" Harmonica Boza", MeasureId = measureML.Id, Price = 1.42m, PreserveCreatedOn = false},
                new PRODUCT {VendorId = bioBulgariaVendor.Id, Name = "Bio Eggs", MeasureId = measureP.Id, Price = 0.8m},
                new PRODUCT {VendorId = bioBulgariaVendor.Id, Name = "Bio Cheese", MeasureId = measureGR.Id, Price = 13.90m},
                new PRODUCT {VendorId = bioBulgariaVendor.Id, Name = "Bio Yoghurt", MeasureId = measureGR.Id, Price = 2.88m},
                
                new PRODUCT {VendorId = mondelezVendor.Id, Name = "Chocolate Milka", MeasureId = measureGR.Id, Price = 2.20m, CreatedOn = new DateTime(2009, 9, 15)},
                new PRODUCT {VendorId = mondelezVendor.Id, Name = "Jacobs Monarch", MeasureId = measureGR.Id, Price = 7.89m, CreatedOn = new DateTime(2010, 5, 13)},
                new PRODUCT {VendorId = mondelezVendor.Id, Name = "Toblerone", MeasureId = measureGR.Id, Price = 4.69m, CreatedOn = new DateTime(2015, 2, 25)},

                new PRODUCT {VendorId = intersnackVendor.Id, Name = "Chio Chips Paprika", MeasureId = measureGR.Id, Price = 3.29m},
                new PRODUCT {VendorId = intersnackVendor.Id, Name = "Pom-Bar", MeasureId = measureGR.Id, Price = 3.70m},

                new PRODUCT {VendorId = bolyarkaVendor.Id, Name = "Beer \"Kaltenberg Pils\"", MeasureId = measureML.Id, Price = 1.7m},
                new PRODUCT {VendorId = bolyarkaVendor.Id, Name = "Beer \"Bolyarka Lager\"", MeasureId = measureML.Id, Price = 1.18m},

                new PRODUCT {VendorId = detelinaVendor.Id, Name = "Row Nuts Mix", MeasureId = measureGR.Id, Price = 4.98m},
                new PRODUCT {VendorId = detelinaVendor.Id, Name = "Dried Fruit", MeasureId = measureGR.Id, Price = 3.68m},

                new PRODUCT {VendorId = sunMoonVendor.Id, Name = "Rye Bread", MeasureId = measureP.Id, Price = 4.77m},
                new PRODUCT {VendorId = sunMoonVendor.Id, Name = "Bread with Olives and dried Tomatoes", MeasureId = measureP.Id, Price = 5.12m},

                new PRODUCT {VendorId = sunMoonVendor.Id, Name = "Zayo-Bayo with Butter", MeasureId = measureP.Id, Price = 0.30m},
                new PRODUCT {VendorId = sunMoonVendor.Id, Name = "Zayo-Bayo with Cream and Onio", MeasureId = measureP.Id, Price = 0.35m},
                new PRODUCT {VendorId = sunMoonVendor.Id, Name = "Zayo-Bayo with Paprika", MeasureId = measureP.Id, Price = 0.35m},
                
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}