namespace SupermarketChain.Apps.JSONReportGenerator
{
    using System;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;

    using Data.DataContext;
    using Data.DataContext.Repositories;
    using Data.Models.SQLServerModels;
    using MongoDB.Bson;
    using MongoDB.Driver;

    using Newtonsoft.Json;

    public class JSONReportGeneratorMain
    {
        private const string OutputDir = "../../../generated-reports/json-reports/";

        public static void Main(string[] args)
        {
            DateTime startDate;// = new DateTime(1300, 01, 01);
            DateTime endDate; //= new DateTime(2040, 01, 01);
            try
            {
                JSONReportGeneratorMain.ValidateInputArguments(args, out startDate, out endDate);
                JSONReportGeneratorMain.GenerateJSONReport(startDate, endDate);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ValidateInputArguments(string[] args, out DateTime startDate, out DateTime endDate)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Arguments list is empty");
            }

            if (!DateTime.TryParse(args[0], out startDate))
            {
                throw new FormatException("Start Date is not in valid format");
            }

            if (!DateTime.TryParse(args[1], out endDate))
            {
                throw new FormatException("End Date is not in valid format");
            }
        }

        private static void GenerateJSONReport(DateTime startDate, DateTime endDate)
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
#pragma warning disable 618
                MongoServer server = client.GetServer();
#pragma warning restore 618
                MongoDatabase db = server.GetDatabase("SupermarketChain");
                var collection = db.GetCollection<string>("SalesByProductReports");

                var products = new GenericRepository<Product>(context);
                var result =
                    products
                    .Find(p => p.Sales.Any(s => DbFunctions.TruncateTime(s.Date) >= startDate && DbFunctions.TruncateTime(s.Date) <= endDate))
                    .ToList();

                foreach (var product in result)
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