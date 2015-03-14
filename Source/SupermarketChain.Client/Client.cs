namespace SupermarketChain.Client
{
    using System;
    using System.Data.Entity;
    using Data.DataContext;
    using Data.DataContext.Migrations.Oracle;
    using Data.DataContext.Repositories;
    using Data.Models.OracleXEModels;
    using Data.Models.SQLServerModels;

    public class Client
    {
        private static void Main()
        {
            bool quit = false;
            while (!quit)
            {
                Console.WriteLine(@"Please choose an option to execute:
Press 1 to work with Oracle Db SupermarketChain.
Press 2 to work with MS SQL Server Db SupermarketChain.
Prezz q to quit the application.");

                var userInput = Convert.ToString(Console.ReadLine()).Trim();

                switch (userInput)
                {
                    case "1":
                        Client.OracleDb();
                        break;
                    case "2":
                        Client.SqlServerDb();
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

        private static void SqlServerDb()
        {
            try
            {
                using (var sqlServerContext = MsDataContext.Create())
                {
                    var measures = new GenericRepository<Measure>(sqlServerContext);

                    var listMeasures = measures.GetAll();

                    foreach (var v in listMeasures)
                    {
                        Console.WriteLine(v.Name);
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private static void OracleDb()
        {
            try
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<OracleDataContext, Configuration>());
                using (var oracleContext = OracleDataContext.Create())
                {
                   

                    string createSequenceVendorTable =
                @"CREATE SEQUENCE VEND_SEQ MINVALUE 10 MAXVALUE 9999999999999999999999999999 INCREMENT BY 10;
create or replace TRIGGER VEND_BIR
BEFORE INSERT ON VENDORS
FOR EACH ROW
BEGIN
SELECT VEND_SEQ.NEXTVAL
INTO :new.id
FROM dual;
END;
";
                    oracleContext.Database.SqlQuery<VENDORS>(createSequenceVendorTable);

                    string createSequenceMeasureTable = @"CREATE SEQUENCE MES_SEQ MINVALUE 100 MAXVALUE 9999999999999999999999999999 INCREMENT BY 100;
create or replace TRIGGER MES_BIR
BEFORE INSERT ON MEASURES
FOR EACH ROW
BEGIN
SELECT MES_SEQ.NEXTVAL
INTO :new.id
FROM dual;
END;";

                    oracleContext.Database.SqlQuery<MEASURES>(createSequenceMeasureTable);

                    string createSequenceProductTable = @"CREATE SEQUENCE PROD_SEQ;
CREATE OR REPLACE TRIGGER PROD_BIR
BEFORE INSERT ON PRODUCTS
FOR EACH ROW
BEGIN
SELECT PROD_SEQ.NEXTVAL
INTO :new.id
FROM dual;
END;";
                    oracleContext.Database.SqlQuery<PRODUCTS>(createSequenceProductTable);

                    //oracleContext.MEASURES.Add(new MEASURES
                    //{
                    //    NAME = "TestingTest",
                    //    ABBREVIATION = "t"
                    //});

                    oracleContext.SaveChanges();

                    //OracleManualMigration.Seeder(oracleContext);

                    var measures = new GenericRepository<MEASURES>(oracleContext);

                    var listMeasures = measures.GetAll();

                    foreach (var v in listMeasures)
                    {
                        Console.WriteLine(v.NAME);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}