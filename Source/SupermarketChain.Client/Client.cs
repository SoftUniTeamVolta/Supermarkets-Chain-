using System.Globalization;
using SupermarketChain.Apps.XmlExpenseImport;
using SupermarketChain.Apps.XmlReportGenerator;

namespace SupermarketChain.Client
{
    using System;

    public class Client
    {
        private static void Main()
        {
            bool quit = false;
            while (!quit)
            {
                Console.WriteLine(@"Please choose an option to execute:
Press 1 to populate Oracle Db SupermarketChain with test data.
Press 2 copy all data from OracleDb to SQL Server.
Press 3 to populate SQL Server Db SupermarketChain with test data.
Press 4 to generate xml report for start date and end date.
Press 5 to import data from xml.
Press q to quit the application.");

                var userInput = Convert.ToString(Console.ReadLine()).Trim();

                switch (userInput)
                {
                    case "1":
                        Commands.CreateOracleDb();
                        break;
                    case "2":
                        Commands.CopyDataTSqlServerDb();
                        break;
                    case "3":
                        Commands.CreateSQLDb();
                        break;
                    case "4":
                        Console.WriteLine("Enter start date and end date on separated lines in fortmat [dd-MM-yyyy]:");

                        DateTime startDate = DateTime.Parse(Console.ReadLine());
                        DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                        XmlReportGenerator.GenerateXmlReport(startDate, endDate);
                        break;

                    case "5":
                        XmlImport.ImportExpenses();
                        break;

                    case "q":
                        quit = true;
                        Console.WriteLine("See you later. Bye bye!");
                        break;
                    default:
                        Console.WriteLine("Invalid input entry. Please press any key to start again.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}