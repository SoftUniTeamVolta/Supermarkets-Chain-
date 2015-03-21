namespace SupermarketChain.Apps.JSONReportGenerator
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.IO;
    using System.Web.Script.Serialization;
    using SupermarketChain.Data.DataContext;
    using SupermarketChain.Data.Models.SQLServerModels;
    using Newtonsoft.Json;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class JSONReportGeneratorMain
    {
        static void Main()
        {
            DateTime startDate = new DateTime(1300, 01, 01);
            DateTime endDate = new DateTime(2040, 01, 01);

            GenerateJSONReport(startDate, endDate);
        }

        public static void GenerateJSONReport(DateTime startDate, DateTime endDate)
        {

            if (!Directory.Exists("../../../generated-files/Json-Reports/"))
            {
                Directory.CreateDirectory("../../../generated-files/Json-Reports/");
            }

            using (var context = new MsDataContext())
            {
                MongoClient client = new MongoClient("mongodb://localhost");
                MongoServer server = client.GetServer();
                MongoDatabase db = server.GetDatabase("test");
                var collection = db.GetCollection<string>("sales");

                var products = context
                    .Products
                    .Where(x => x.Sales
                        .Any(s => s.Date >= startDate && s.Date <= endDate)
                    )
                    .Include(v => v.Sales);

                foreach (var product in products)
                {
                    var sales = product.Sales;

                    decimal quantitySold = 0;
                    decimal totalSum = 0;

                    foreach (var sale in sales)
                    {
                        quantitySold += sale.Quantity;
                        totalSum += sale.Sum;
                    }
                   
                    var productJSON = JsonConvert.SerializeObject(new
                    {
                        productId = product.Id,
                        productName = product.Name,
                        vendorName = product.Vendor.Name,
                        totalQuantitySold = quantitySold,
                        totalIncomes = totalSum
                    }, Formatting.Indented);

                    File.WriteAllText("../../../generated-files/Json-Reports/" + product.Id + ".json", productJSON);

                    collection.Insert(BsonDocument.Parse(productJSON));
                }

                Console.WriteLine("JSON report has been generated!");
                Console.WriteLine("Json objects added to db 'test' succesfully");
                Console.WriteLine();
            }
        }
    }
}
