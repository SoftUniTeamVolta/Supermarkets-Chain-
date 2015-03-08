namespace SupermarketChain.Services.OracleExporter
{
    using System.IO;

    public class ProductReport
    {
        public string Name { get; set; }
        public Stream FileStream { get; set; }
    }
}
