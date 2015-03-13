using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketChain.Data.DataContext.Migrations
{
    using Models.OracleXEModels;

    public static class OracleManualMigration
    {
        public static void Seeder(OracleDataContext context)
        {
            if (!context.MEASURES.Any())
            {
                OracleManualMigration.AddMeasureTestData(context);
            }

            if (!context.VENDORS.Any())
            {
                OracleManualMigration.AddVendorTestData(context);
            }

            if (!context.PRODUCTS.Any())
            {
                OracleManualMigration.AddProductTestData(context);
            }
        }

        private static void AddVendorTestData(OracleDataContext context)
        {
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
            var vendor = context.VENDORS.FirstOrDefault(v => v.NAME == "Zagorka SA");
            var measure = context.MEASURES.FirstOrDefault(m => m.NAME == "liters");
            var products = new List<PRODUCTS>
            {
                new PRODUCTS {VENDOR_ID = vendor.ID, NAME = "Beer \"Zagorka\"", MEASURE_ID = measure.ID}
            };

            products.ForEach(p => context.PRODUCTS.Add(p));
            context.SaveChanges();
        }
    }
}
