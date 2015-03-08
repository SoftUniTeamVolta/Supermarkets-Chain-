namespace SupermarketChain.Services.OracleExporter
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IOracleExporter
    {
        [OperationContract]
        string Test();

        [OperationContract]
        string GetReportAll();

        [OperationContract]
        string GetReportByDate(DateTime date);
    }
}
