namespace SupermarketChain.Client
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.IO.Compression;
    using System.Net.Mime;
    using System.Xml;
    using AutoMapper;
    using AutoMapper.Internal;
    using Data.DataContext;
    using Data.DataContext.Migrations.Oracle;
    using Data.DataContext.Repositories;

    using Data.Models.OracleXEModels;
    using Data.Models.SQLServerModels;
    using Excel;

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
                              .ForMember(v => v.Products, opt => opt.MapFrom(p => p.Products))
                              .ForMember(v => v.Sales, opt => opt.MapFrom(s => s.Sales))
                              .ForMember(v => v.Expenses, opt => opt.Ignore());
                              //.ForMember(v => v.Products, opt => opt.Ignore())

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
                    // What if we have products with different CreatedOn dates?
                    //var first = oracleEntity.GetFirstEntry();
                    //uniqueOracleProducts = oracleEntity.Find(v => v.CreatedOn == first.CreatedOn);

                    //Better get all products to be sure that all products will transfer to MsDB
                    uniqueOracleProducts = oracleEntity.GetAll();
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
                    // What if we have measures with different CreatedOn dates?
                    //var first = oracleEntity.GetFirstEntry();
                    //uniqueOracleMeasures = oracleEntity.Find(v => v.CreatedOn == first.CreatedOn);

                    //Better get all measures to be sure that all measures will transfer to MsDB
                    uniqueOracleMeasures = oracleEntity.GetAll();
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
                    // What if we have vendors with different CreatedOn dates?
                    //var first = oracleEntity.GetFirstEntry();
                    //uniqueOracleVendors = oracleEntity.Find(v => v.CreatedOn == first.CreatedOn);

                    //Better get all vendors to be sure that all vendors will transfer to MsDB
                    uniqueOracleVendors = oracleEntity.GetAll();
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

        public static void CreateMySQLDb()
        {
            try
            {
                using (var context = new MySQLContext())
                {
                    context.SaveChanges();
                    Console.WriteLine("MySQL database generated!");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void LoadExcelReportsToSqlServer()
        {
            string zipPath = "../../../files-for-import/Sample-Sales-Reports.zip";

            try
            {
                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Read))
                {

                    var entries = archive.Entries;
                    foreach (var entry in entries)
                    {
                        if (entry.ToString().Contains(".xls"))
                        {
                            ZipArchiveEntry sample = archive.GetEntry(entry.ToString());
                            Stream excelZip = sample.Open();
                            MemoryStream stream = new MemoryStream();
                            excelZip.CopyTo(stream);
                            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                            DataSet result = excelReader.AsDataSet();
                            string zipAsXML = result.GetXml();

                            XmlDocument doc = new XmlDocument();
                            //doc.Load(zipAsXML);

                            XmlReader reader = XmlReader.Create(zipAsXML);

                            //using (XmlReader reader = XmlReader.Create(zipAsXML))
                            //{
                            //    while (reader.Read())
                            //    {
                            //        if ((reader.NodeType == XmlNodeType.Element) &&
                            //        (reader.Name == "Column2"))
                            //        {
                            //            Console.WriteLine(reader.ReadElementString());
                            //        }
                            //    }
                            //}

                            Console.WriteLine(zipAsXML);
                            foreach (DataTable table in result.Tables)
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    foreach (var index in row.ItemArray)
                                    {
                                        var currentIndex = index as string;
                                        if (currentIndex.Contains("Supermarket"))
                                        {

                                        }
                                    }
                                }
                            }

                        }

                        Console.WriteLine(entry);
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}