namespace SupermarketChain.Data.Common
{
    public class OracleProduct
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Sum
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }
    }
}
