namespace SupermarketChain.Apps.JSONReportGenerator
{
    using System;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;

    using Data.DataContext;

    using MongoDB.Bson;
    using MongoDB.Driver;

    using Newtonsoft.Json;

    public class JSONReportGeneratorMain
    {
        private const string OutputDir = "../../../generated-reports/json-reports/";

        private static void Main()
        {
            DateTime startDate = new DateTime(1300, 01, 01);
            DateTime endDate = new DateTime(2040, 01, 01);

            JSONReportGeneratorMain.GenerateJSONReport(startDate, endDate);
        }

        public static void GenerateJSONReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                Directory.CreateDirectory(JSONReportGeneratorMain.OutputDir);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {

                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
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

                    File.WriteAllText(JSONReportGeneratorMain.OutputDir + product.Id + ".json", productJSON);

                    collection.Insert(BsonDocument.Parse(productJSON));
                }

                Console.WriteLine("JSON report has been generated!");
                Console.WriteLine("Json objects added to db 'test' succesfully");
                Console.WriteLine();
            }
        }
    }
}