namespace SupermaketChain.Services.OracleExporter
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IOracleExporter
    {
        [OperationContract]
        string Test();
    }
}
