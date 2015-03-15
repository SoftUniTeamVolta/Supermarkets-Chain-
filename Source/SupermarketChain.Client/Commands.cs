namespace SupermarketChain.Client
{
    using System;
    using System.Linq;

    using Data.DataContext;
    using Data.DataContext.Repositories;

    using Data.Models.OracleXEModels;
    using Data.Models.SQLServerModels;

    public static class Commands
    {
        public static void CopyDataTSqlServerDb()
        {
            try
            {
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsDataContext, SupermarketChain.Data.DataContext.Migrations.SQLServer.Configuration>());
                using (var sqlServerContext = new MsDataContext())
                {
                    using (var oracleContext = new OracleDataContext())
                    {
                        var oracleMeasures = new GenericRepository<MEASURES>(oracleContext);
                        var oracleVendors = new GenericRepository<VENDORS>(oracleContext);
                        var oracleProducts = new GenericRepository<PRODUCTS>(oracleContext);

                        var sqlMeasures = new GenericRepository<Measure>(sqlServerContext);
                        var sqlVendors = new GenericRepository<Vendor>(sqlServerContext);
                        var sqlProducts = new GenericRepository<Product>(sqlServerContext);

                        Commands.CheckAndPopulateVendorsCount(oracleVendors, sqlVendors);
                        Commands.CheckAndPopulateMeasuresCount(oracleMeasures, sqlMeasures);
                        Commands.CheckAndPopulateProductsCount(oracleProducts, sqlProducts);

                        //var listMeasures = sqlMeasures.GetAll();
                        //foreach (var v in listMeasures)
                        //{
                        //    Console.WriteLine(v.Name);
                        //}

                        //var products = new GenericRepository<Product>(sqlServerContext);
                        //var productList = products.GetAll();
                        //var first = productList.First();
                        //first.Price = 1.50m;
                        //products.Update(first);
                        //products.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void CheckAndPopulateProductsCount(GenericRepository<PRODUCTS> oracleEntity
                                                          , GenericRepository<Product> sqlEntity
            )
        {
            if (oracleEntity.GetAll().Count() != sqlEntity.GetAll().Count())
            {
                var lastSqlEntity = sqlEntity.GetLatestEntry();
                var uniqueOracleVendors = oracleEntity.Find(v => v.CreatedOn > lastSqlEntity.CreatedOn);
                foreach (var entity in uniqueOracleVendors)
                {
                    var product = new Product
                    {
                        VendorId = entity.VENDOR_ID,
                        Name = entity.NAME,
                        MeasureId = entity.MEASURE_ID
                    };
                    sqlEntity.Add(product);
                }

                sqlEntity.SaveChanges();
            }
        }

        private static void CheckAndPopulateMeasuresCount(GenericRepository<MEASURES> oracleEntity,
                                                          GenericRepository<Measure> sqlEntity)
        {
            if (oracleEntity.GetAll().Count() != sqlEntity.GetAll().Count())
            {
                var lastSqlEntity = sqlEntity.GetLatestEntry();
                var uniqueOracleVendors = oracleEntity.Find(v => v.CreatedOn > lastSqlEntity.CreatedOn);
                foreach (var entity in uniqueOracleVendors)
                {
                    var measure = new Measure
                    {
                        Name = entity.NAME,
                        Abbreviation = entity.ABBREVIATION
                    };
                    sqlEntity.Add(measure);
                }

                sqlEntity.SaveChanges();
            }
        }

        private static void CheckAndPopulateVendorsCount(GenericRepository<VENDORS> oracleEntity,
                                                         GenericRepository<Vendor> sqlEntity)
        {
            if (oracleEntity.GetAll().Count() != sqlEntity.GetAll().Count())
            {
                var lastSqlEntity = sqlEntity.GetLatestEntry();
                var uniqueOracleVendors = oracleEntity.Find(v => v.CreatedOn > lastSqlEntity.CreatedOn);
                foreach (var entity in uniqueOracleVendors)
                {
                    var vendor = new Vendor
                    {
                        Name = entity.NAME
                    };
                    sqlEntity.Add(vendor);
                }

                sqlEntity.SaveChanges();
            }
        }

        public static void OracleDb()
        {
            try
            {
                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<OracleDataContext, Configuration>());
                using (var oracleContext = new OracleDataContext())
                {
                    var oracleVendors = new GenericRepository<VENDORS>(oracleContext);
                    //var measures = new GenericRepository<MEASURES>(oracleContext);

                    //var listMeasures = measures.GetAll();

                    //foreach (var v in listMeasures)
                    //{
                    //    Console.WriteLine(v.NAME);
                    //}

                    //var vendor = new VENDORS
                    //{
                    //    NAME = "PESHO"
                    //};
                    //oracleVendors.Add(vendor);
                    //oracleVendors.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}