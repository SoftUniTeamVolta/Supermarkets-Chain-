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

    public class OracleExporter : IOracleExporter
    {
        private DeletableEntityRepository<Product> products;

        public OracleExporter()
            : this(new DeletableEntityRepository<Product>(new DataContext()))
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


        public string GetReportAll()
        {
            throw new System.NotImplementedException();
        }

        public string GetReportByDate(DateTime date)
        {
            var productsByDate = this.products.All().Where(p => DbFunctions.TruncateTime(p.CreatedOn) == date);
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
            throw new NotImplementedException();
        }
    }
}
