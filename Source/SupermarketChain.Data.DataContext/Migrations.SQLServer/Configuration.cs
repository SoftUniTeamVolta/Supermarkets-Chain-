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

            if (!context.SuperMarkets.Any())
            {
                Configuration.AddSuperMarketsTestData(context);
            }

            if (!context.Sales.Any())
            {
                Configuration.AddSalesTestData(context);
            }
        }

        private static void AddSuperMarketsTestData(MsDataContext context)
        {
            var supermarkets = new List<SuperMarket>
            {
                new SuperMarket {Name = "Pri bay Ivan"},
                new SuperMarket {Name = "Pri bay gosho"}
            };

            supermarkets.ForEach(s => context.SuperMarkets.Add(s));
            context.SaveChanges();
        }

        private static void AddSalesTestData(MsDataContext context)
        {
            var zagorkaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Zagorka SA");
            var superMarket = context.SuperMarkets.First();

            var sales = new List<Sale>
            {
                new Sale
                {
                    Vendor = zagorkaVendor,
                    ProductId = zagorkaVendor.Products.First().Id,
                    SuperMarket = superMarket,
                    UnitPrice = 1.50m,
                    Quantity = 500
                }
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
            var vendor = context.Vendors.FirstOrDefault(v => v.Name == "Zagorka SA");
            var measure = context.Measures.FirstOrDefault(m => m.Name == "liter");
            var products = new List<Product>
            {
                new Product {VendorId = vendor.Id, Name = "Beer \"Zagorka\"", MeasureId = measure.Id}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }
    }
}
