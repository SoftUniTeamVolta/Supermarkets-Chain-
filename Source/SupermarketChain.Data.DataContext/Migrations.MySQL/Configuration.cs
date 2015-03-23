namespace SupermarketChain.Data.DataContext.Migrations.MySQL
{
    using SupermarketChain.Data.Models.MySQLModels;
    using SupermarketChain.Data.Models.SQLServerModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SupermarketChain.Data.DataContext.MySQLContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"Migrations.MySQL";
        }

        protected override void Seed(MySQLContext context)
        {
            if (!context.Measures.Any())
            {
                //Uncomment to populate MsSQL db when do not have Oracle server
                Configuration.AddMeasureTestData(context);
            }

            if (!context.Vendors.Any())
            {
                //Uncomment to populate MsSQL db when do not have Oracle server
                Configuration.AddVendorTestData(context);
            }

            if (!context.Products.Any())
            {
                //Uncomment to populate MsSQL db when do not have Oracle server
                Configuration.AddProductTestData(context);
            }

            //if (!context.Expenses.Any())
            //{
            //    //Uncomment to populate MsSQL db
            //    Configuration.AddExpensesTestData(context);
            //}

            if (!context.Incomes.Any())
            {
                //Uncomment to populate MsSQL db
                //var msContext = new MsDataContext();
                //Configuration.AddIncomesTestData(context, msContext);
            }
        }


        private static void AddMeasureTestData(MySQLContext context)
        {
            var measures = new List<SupermarketChain.Data.Models.MySQLModels.Measure>
            {
                new SupermarketChain.Data.Models.MySQLModels.Measure {Name = "milliliter", Abbreviation = "ml"},
                new SupermarketChain.Data.Models.MySQLModels.Measure {Name = "liter", Abbreviation = "l"},
                new SupermarketChain.Data.Models.MySQLModels.Measure {Name = "gram", Abbreviation = "gr"},
                new SupermarketChain.Data.Models.MySQLModels.Measure {Name = "kilogram", Abbreviation = "kg"},
                new SupermarketChain.Data.Models.MySQLModels.Measure {Name = "piece", Abbreviation = "pc"}
            };

            measures.ForEach(m => context.Measures.Add(m));
            context.SaveChanges();
        }


        private static void AddVendorTestData(MySQLContext context)
        {
            var vendors = new List<SupermarketChain.Data.Models.MySQLModels.Vendor>
            {
                new SupermarketChain.Data.Models.MySQLModels.Vendor {Name = "Zagorka SA"},
                new SupermarketChain.Data.Models.MySQLModels.Vendor {Name = "Kamenitza SA"},
                new SupermarketChain.Data.Models.MySQLModels.Vendor {Name = "Bio Bulgaria Ltd."},
                new SupermarketChain.Data.Models.MySQLModels.Vendor {Name = "Mondelez Bulgaria Ltd."},
                new SupermarketChain.Data.Models.MySQLModels.Vendor {Name = "Intersnack Bulgaria Ltd."}
            };

            vendors.ForEach(v => context.Vendors.Add(v));
            context.SaveChanges();
        }


        private static void AddProductTestData(MySQLContext context)
        {
            var zagorkaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Zagorka SA");
            var kamentzaVendor = context.Vendors.FirstOrDefault(v => v.Name == "Kamenitza SA");
            var measure = context.Measures.FirstOrDefault(m => m.Name == "liter");
            var products = new List<SupermarketChain.Data.Models.MySQLModels.Product>
            {
                new SupermarketChain.Data.Models.MySQLModels.Product {VendorId = zagorkaVendor.Id, Name = "Beer \"Zagorka\"", MeasureId = measure.Id},
                new SupermarketChain.Data.Models.MySQLModels.Product {VendorId = kamentzaVendor.Id, Name = "Beer \"Kamenitza\"", MeasureId = measure.Id}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();
        }

    }
}
