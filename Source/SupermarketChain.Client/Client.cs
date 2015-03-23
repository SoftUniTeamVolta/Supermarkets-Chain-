namespace SupermarketChain.Client
{
    using System;
    using System.Globalization;

    using Apps.JSONReportGenerator;
    using Apps.PdfReportGenerator;
    using Apps.XmlExpenseImport;
    using Apps.XmlReportGenerator;
    using System.Threading;

    public class Client
    {
        private static void Main()
        {
            bool quit = false;
            while (!quit)
            {
                Console.WriteLine(@"Please choose an option to execute:
Press 1 to populate Oracle Db SupermarketChain with test data.
Press 2 to copy all data from OracleDb to SQL Server.
Press 3 to generate Pdf report for specific period.
Press 4 to generate xml report for start date and end date.
Press 5 to generate JSON report for start date and end date.
Press 6 to import data from xml.
Press 7 to populate MySQL Server Db SupermarketChain with test data
Press q to quit the application.");

                var userInput = Convert.ToString(Console.ReadLine()).Trim();
                Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

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
                        Console.WriteLine("Enter start date and end date in format [dd-MM-yyyy]:");
                        Console.WriteLine(
                            "If you do not enter start and/or end date, a sales report for the current date will be generated.");
                        Console.Write("Start date:");
                        string startDateStr = Console.ReadLine();
                        Console.Write("End date:");
                        string endDateStr = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(startDateStr))
                        {
                            startDateStr = DateTime.Now.ToShortDateString();

                        }
                        if (string.IsNullOrWhiteSpace(endDateStr))
                        {
                            endDateStr = DateTime.Now.ToShortDateString();
                        }

                        string[] args = { startDateStr, endDateStr };
                        PdfGeneratorMain.Main(args);
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine("Enter start date and end date on separated lines in fortmat [dd-MM-yyyy]:");

                        DateTime startDate = DateTime.Parse(Console.ReadLine());
                        //DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy",
                        //    CultureInfo.InvariantCulture);
                        DateTime endDate = DateTime.Parse(Console.ReadLine());

                        XmlReportGenerator.GenerateXmlReport(startDate, endDate);
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "5":
                        Console.WriteLine("Enter start date and end date on separated lines in fortmat [dd-MM-yyyy]:");

                        startDate = DateTime.Parse(Console.ReadLine());
                        //endDate = DateTime.ParseExact(Console.ReadLine(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        endDate = DateTime.Parse(Console.ReadLine());
                        JSONReportGeneratorMain.GenerateJSONReport(startDate, endDate);
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "6":
                        XmlImport.ImportExpenses();
                        Console.WriteLine("Press any key to return to the initial menu.");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "7":
                        Commands.CreateMySQLDb();
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