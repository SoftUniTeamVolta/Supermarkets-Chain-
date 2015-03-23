namespace SupermarketChain.Data.DataContext.Migrations.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models.SQLServerModels;

    public sealed class Configuration : DbMigrationsConfiguration<SupermarketChain.Data.DataContext.MsDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"Migrations.SQLServer";
        }

        protected override void Seed(MsDataContext context)
        {
            if (!context.Measures.Any())
            {
                // Uncomment to populate MsSQL db when do not have Oracle server
                // or move seed methods directly to Oracle Configuration.cs
                // Configuration.AddMeasureTestData(context);
            }

            if (!context.Vendors.Any())
            {
                // Uncomment to populate MsSQL db when do not have Oracle server
                // or move seed methods directly to Oracle Configuration.cs
                // Configuration.AddVendorTestData(context);
            }

            if (!context.Products.Any())
            {
                // Uncomment to populate MsSQL db when do not have Oracle server
                // or move seed methods directly to Oracle Configuration.cs
                // Configuration.AddProductTestData(context);
            }

            if (!context.SuperMarkets.Any())
            {
                // Uncomment method and run task 2 again to prepare for task 3
                // or move seed methods directly to Oracle Configuration.cs
                // Configuration.AddSuperMarketsTestData(context);
            }

            if (!context.Sales.Any())
            {
                // Uncomment method and run task 2 again to prepare for task 3
                // or move seed methods directly to Oracle Configuration.cs
                // Configuration.AddSalesTestData(context);
            }
        }

        private static void AddSuperMarketsTestData(MsDataContext context)
        {
            var supermarkets = new List<Supermarket>
            {
                new Supermarket {Name = "Pikadili Lozenets"},
                new Supermarket {Name = "Billa Lozenetz"},
                new Supermarket {Name = "Carrefur Mall Sofia"},
                new Supermarket {Name = "Laika"},
                new Supermarket {Name = "Biomag Journalist Square"},
                new Supermarket {Name = "Frodo Zlatovrah"},
                new Supermarket {Name = "Fantastiko Students Town"}
            };

            supermarkets.ForEach(s => context.SuperMarkets.Add(s));
            context.SaveChanges();
        }

        private static void AddSalesTestData(MsDataContext context)
        {
            var zagorkaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Zagorka SA");
            var superMarket = context.SuperMarkets.First();

            var kamenitzaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Kamenitza SA");

            var sales = new List<Sale>
            {
                new Sale(zagorkaVendor, zagorkaVendor.Products.First().Id, superMarket, 1.50m, 250),
                new Sale(zagorkaVendor, zagorkaVendor.Products.First().Id, superMarket, 1.50m, 345),
                new Sale(zagorkaVendor, zagorkaVendor.Products.First().Id, superMarket, 1.50m, 150),
                new Sale(kamenitzaVendor, kamenitzaVendor.Products.First().Id, superMarket, 1.50m, 500),
                new Sale(kamenitzaVendor, kamenitzaVendor.Products.First().Id, superMarket, 1.50m, 600),
                new Sale(kamenitzaVendor, kamenitzaVendor.Products.First().Id, superMarket, 1.50m, 240)
            };

            sales.ForEach(s => context.Sales.Add(s));
            context.SaveChanges();
        }

        private static void AddVendorTestData(MsDataContext context)
        {
            var vendors = new List<Vendor>
            {
                new Vendor {Name = "Zagorka SA"},
                new Vendor {Name = "Kamenitza SA"},
                new Vendor {Name = "Bio Bulgaria Ltd."},
                new Vendor {Name = "Mondelez Bulgaria Ltd."},
                new Vendor {Name = "Intersnack Bulgaria Ltd."}
            };

            vendors.ForEach(v => context.Vendors.Add(v));
            context.SaveChanges();
        }

        private static void AddMeasureTestData(MsDataContext context)
        {
            var measures = new List<Measure>
            {
                new Measure {Name = "milliliter", Abbreviation = "ml"},
                new Measure {Name = "liter", Abbreviation = "l"},
                new Measure {Name = "gram", Abbreviation = "gr"},
                new Measure {Name = "kilogram", Abbreviation = "kg"},
                new Measure {Name = "piece", Abbreviation = "pc"}
            };

            measures.ForEach(m => context.Measures.Add(m));
            context.SaveChanges();
        }

        private static void AddProductTestData(MsDataContext context)
        {
            var zagorkaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Zagorka SA");
            var kamentzaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Kamenitza SA");
            var measure = context.Measures.FirstOrDefault(m => m.Name == "liter");
            var products = new List<Product>
            {
                new Product {VendorId = zagorkaVendor.Id, Name = "Beer \"Zagorka\"", MeasureId = measure.Id},
                new Product {VendorId = kamentzaVendor.Id, Name = "Beer \"Kamenitza\"", MeasureId = measure.Id}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}
