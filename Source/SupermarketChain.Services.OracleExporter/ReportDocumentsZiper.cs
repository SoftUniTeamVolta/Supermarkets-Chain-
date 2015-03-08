namespace SupermarketChain.Services.OracleExporter
{
    using System;
    using System.Collections.Generic;

    public class ReportDocumentsZiper : IDisposable
    {
        internal void CreateTempFileStructure()
        {
            throw new System.NotImplementedException();
        }

        internal void InsertReports(ICollection<ProductReport> reports)
        {
            throw new System.NotImplementedException();
        }

        internal void ZipInsertedFiles()
        {
            throw new System.NotImplementedException();
        }

        public object GetZipedReports()
        {
            // Change return type later / some kind of ZIP file type /
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // delete files and folder generated for the report
            // zip file also
            throw new NotImplementedException();
        }
    }
}