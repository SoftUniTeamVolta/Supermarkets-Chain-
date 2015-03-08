namespace SupermarketChain.Services.Hosts.OracleExporterHost
{
    using System;
    using System.Linq;
    using System.ServiceModel;

    using SupermarketChain.Services.OracleExporter;
    using SupermarketChain.Common;

    public static class OracleExporterHostMain
    {
        static void Main()
        {
            using (var host = new ServiceHost(typeof(OracleExporter)))
            {
                host.Open();
                Logger.Instance.Information("OracleExporter successfully opened.");
                Console.WriteLine("{0} running at", host.Description.Name);
                Console.WriteLine(string.Join(Environment.NewLine, host.Description.Endpoints.Select(e => e.Address)));
                Console.ReadKey();
                try
                {
                    host.Close();
                    Logger.Instance.Information("OracleExporter successfully closed.");
                }
                catch (FaultException fe)
                {
                    host.Abort();
                    Logger.Instance.Error(fe);
                }
            }
        }
    }
}
