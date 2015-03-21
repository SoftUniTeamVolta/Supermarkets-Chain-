using System.Globalization;
using SupermarketChain.Apps.JSONReportGenerator;
using SupermarketChain.Apps.XmlExpenseImport;
using SupermarketChain.Apps.XmlReportGenerator;

namespace SupermarketChain.Client
{
    using System;
    using Apps.PdfReportGenerator;

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
Press 6 to generate JSON report for start date and end date.
Press 7 to generate Pdf report for specific period.
Press q to quit the application.");

                var userInput = Convert.ToString(Console.ReadLine()).Trim();

                switch (userInput)
                {
                    case "1":
                        Commands.CreateOracleDb();
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        Commands.CopyDataTSqlServerDb();
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        Commands.CreateSQLDb();
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine("Enter start date and end date on separated lines in fortmat [dd-MM-yyyy]:");

                        DateTime startDate = DateTime.Parse(Console.ReadLine());
                        DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                        XmlReportGenerator.GenerateXmlReport(startDate, endDate);
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "5":
                        XmlImport.ImportExpenses();
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "6":
                        Console.WriteLine("Enter start date and end date on separated lines in fortmat [dd-MM-yyyy]:");

                        startDate = DateTime.Parse(Console.ReadLine());
                        endDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        JSONReportGeneratorMain.GenerateJSONReport(startDate, endDate);
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "7":
                        Console.WriteLine("Enter start date and end date in format [dd-MM-yyyy]:");
                        Console.WriteLine("If you do not enter start and/or end date, a sales report for the current date will be generated.");
                        Console.Write("Start date:");
                        string startDateStr = Console.ReadLine();
                        Console.Write("End date:");
                        string endDateStr = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(startDateStr) || string.IsNullOrWhiteSpace(endDateStr))
                        {
                            startDateStr = DateTime.Now.ToShortDateString();
                            endDateStr = DateTime.Now.ToShortDateString();
                        }

                        string[] args = {startDateStr, endDateStr};
                        PdfGeneratorMain.Main(args);
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
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