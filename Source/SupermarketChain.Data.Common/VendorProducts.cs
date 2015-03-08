namespace SupermarketChain.Data.Common
{
    using System.Collections.Generic;

    public class VendorProducts
    {
        public VendorProducts()
        {
            this.Products = new HashSet<OracleProduct>();
        }

        public string VendorName { get; set; }
        public ICollection<OracleProduct> Products { get; set; }
    }
}
