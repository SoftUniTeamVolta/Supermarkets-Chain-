namespace SupermarketChain.Services.OracleExporter
{
    using SupermarketChain.Data.Common;
    using SupermarketChain.Data.DataContext;
    using SupermarketChain.Data.DataContext.Repositories;
    using SupermarketChain.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Data.Models.SQLServerModels;

    public class OracleExporter : IOracleExporter
    {
        private DeletableEntityRepository<Product> products;

        public OracleExporter()
            : this(new DeletableEntityRepository<Product>(new MsDataContext()))
        {
            // Poor man DI :(
        }

        public OracleExporter(DeletableEntityRepository<Product> products)
        {
            this.products = products;
        }

        public string Test()
        {
            return "OK";
        }


        public object GetReportsAll()
        {
            List<DateTime?> distinctDates = this.products.GetAll().Select(p => DbFunctions.TruncateTime(p.CreatedOn)).Distinct().ToList();
            ReportFactory reportFactory = new ReportFactory();
            ICollection<ProductReport> reports = new List<ProductReport>();
            foreach(DateTime? date in distinctDates)
            {
                IEnumerable<VendorProducts> productsData = this.GetReportDataByDate((DateTime)date);
                ProductReport report = reportFactory.CreateReport(productsData);
                reports.Add(report);
            }

            return this.ZipGeneratedReports(reports);
        }

        private IEnumerable<VendorProducts> GetReportDataByDate(DateTime date)
        {
            var productsByDate = this.products.GetAll().Where(p => DbFunctions.TruncateTime(p.CreatedOn) == date);
            var vendorNames = productsByDate.Select(p => p.Vendor.Name).ToList().Distinct();
            IEnumerable<VendorProducts> vendorProducts = vendorNames.Select(v => new VendorProducts
                {
                    VendorName = v,
                    Products = productsByDate.Where(p => p.Vendor.Name == v).Select(p => new OracleProduct
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Quantity = productsByDate.Count(pd => pd.Name == p.Name),
                    }).ToList(),
                });

            return vendorProducts;
        }

        private object ZipGeneratedReports(ICollection<ProductReport> reports)
        {
            using(ReportDocumentsZiper ziper = new ReportDocumentsZiper())
            {
                ziper.CreateTempFileStructure();
                ziper.InsertReports(reports);
                ziper.ZipInsertedFiles();

                return ziper.GetZipedReports();
            }
        }
    }
}
