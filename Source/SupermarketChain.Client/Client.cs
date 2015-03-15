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
Prezz q to quit the application.");

                var userInput = Convert.ToString(Console.ReadLine()).Trim();

                switch (userInput)
                {
                    case "1":
                        Commands.OracleDb();
                        break;
                    case "2":
                        Commands.CopyDataTSqlServerDb();
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