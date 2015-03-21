namespace SupermarketChain.Client
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.Internal;
    using Data.DataContext;
    using Data.DataContext.Migrations.Oracle;
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
                        var oracleMeasures = new GenericRepository<MEASURE>(oracleContext);
                        var oracleVendors = new GenericRepository<VENDOR>(oracleContext);
                        var oracleProducts = new GenericRepository<PRODUCT>(oracleContext);

                        var sqlMeasures = new GenericRepository<Measure>(sqlServerContext);
                        var sqlVendors = new GenericRepository<Vendor>(sqlServerContext);
                        var sqlProducts = new GenericRepository<Product>(sqlServerContext);

                        Mapper.CreateMap<MEASURE, Measure>();
                        Mapper.CreateMap<VENDOR, Vendor>()
                              //.ForMember(v => v.Products, opt => opt.MapFrom(p => p.Products))
                              .ForMember(v => v.Sales, opt => opt.MapFrom(s => s.Sales))
                              .ForMember(v => v.Expenses, opt => opt.Ignore())
                              .ForMember(v => v.Products, opt => opt.Ignore());

                        Mapper.CreateMap<PRODUCT, Product>()
                              .ForMember(p => p.Measure, opt => opt.Ignore())
                              .ForMember(p => p.Vendor, opt => opt.Ignore());
                        Mapper.CreateMap<SALE, Sale>();
                        Mapper.CreateMap<SUPERMARKET, Supermarket>();
                        Mapper.AssertConfigurationIsValid();

                        Commands.CheckAndPopulateMeasuresCount(oracleMeasures, sqlMeasures);
                        Commands.CheckAndPopulateVendorsCount(oracleVendors, sqlVendors);
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

        private static void CheckAndPopulateProductsCount(GenericRepository<PRODUCT> oracleEntity
                                                          , GenericRepository<Product> sqlEntity)
        {
            var oracleCount = oracleEntity.GetAll().Count();
            var sqlServerCount = sqlEntity.GetAll().Count();
            if (oracleCount != sqlServerCount)
            {
                var lastSqlEntity = sqlEntity.GetLatestEntry();
                IEnumerable<PRODUCT> uniqueOracleProducts; 
                if (lastSqlEntity == null)
                {
                    var first = oracleEntity.GetFirstEntry();
                    uniqueOracleProducts = oracleEntity.Find(v => v.CreatedOn == first.CreatedOn);
                }
                else
                {
                    uniqueOracleProducts = oracleEntity.Find(v => v.CreatedOn > lastSqlEntity.CreatedOn);
                }

                foreach (var entity in uniqueOracleProducts)
                {
                    var product = Mapper.Map<PRODUCT, Product>(entity);
                    sqlEntity.Add(product);
                }

                sqlEntity.SaveChanges();
            }
        }



        private static void CheckAndPopulateMeasuresCount(GenericRepository<MEASURE> oracleEntity,
                                                          GenericRepository<Measure> sqlEntity)
        {
            var oracleCount = oracleEntity.GetAll().Count();
            var sqlServerCount = sqlEntity.GetAll().Count();
            if (oracleCount != sqlServerCount)
            {
                var lastSqlEntity = sqlEntity.GetLatestEntry();
                IEnumerable<MEASURE> uniqueOracleMeasures;
                if (lastSqlEntity == null)
                {
                    var first = oracleEntity.GetFirstEntry();
                    uniqueOracleMeasures = oracleEntity.Find(v => v.CreatedOn == first.CreatedOn);
                }
                else
                {
                    uniqueOracleMeasures = oracleEntity.Find(v => v.CreatedOn > lastSqlEntity.CreatedOn);
                }

                foreach (var entity in uniqueOracleMeasures)
                {
                    var measure = Mapper.Map<MEASURE, Measure>(entity);
                    sqlEntity.Add(measure);
                }

                sqlEntity.SaveChanges();
            }
        }

        private static void CheckAndPopulateVendorsCount(GenericRepository<VENDOR> oracleEntity,
                                                         GenericRepository<Vendor> sqlEntity)
        {
            var oracleCount = oracleEntity.GetAll().Count();
            var sqlServerCount = sqlEntity.GetAll().Count();
            if (oracleCount != sqlServerCount)
            {
                var lastSqlEntity = sqlEntity.GetLatestEntry();

                IEnumerable<VENDOR> uniqueOracleVendors;
                if (lastSqlEntity == null)
                {
                    var first = oracleEntity.GetFirstEntry();
                    uniqueOracleVendors = oracleEntity.Find(v => v.CreatedOn == first.CreatedOn);
                }
                else
                {
                    uniqueOracleVendors = oracleEntity.Find(v => v.CreatedOn > lastSqlEntity.CreatedOn);
                }
                
                
                foreach (var entity in uniqueOracleVendors)
                {
                    var vendor = Mapper.Map<VENDOR, Vendor>(entity);
                    sqlEntity.Add(vendor);
                }

                sqlEntity.SaveChanges();
            }
        }

        public static void CreateOracleDb()
        {
            try
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<OracleDataContext, Configuration>());
                //Database.SetInitializer(new CreateDatabaseIfNotExists<OracleDataContext>());
                using (var oracleContext = new OracleDataContext())
                {
                    //var oracleVendors = new GenericRepository<VENDOR>(oracleContext);
                    //var measures = new GenericRepository<MEASURE>(oracleContext);

                    //var listMeasures = measures.GetAll();

                    //foreach (var v in listMeasures)
                    //{
                    //    Console.WriteLine(v.Name);
                    //}

                    //var vendor = new VENDOR
                    //{
                    //    Name = "PESHO"
                    //};
                    //oracleVendors.Add(vendor);
                    //oracleVendors.SaveChanges();
                    oracleContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void CreateSQLDb()
        {
            try
            {
                using (var context = new MsDataContext())
                {
                    context.SaveChanges();
                    Console.WriteLine("SQL Server database generated!");
                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}