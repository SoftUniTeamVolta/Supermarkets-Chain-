namespace SupermarketChain.Services.OracleExporter
{
    using SupermarketChain.Data.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ReportFactory
    {
        internal ProductReport CreateReport(IEnumerable<VendorProducts> productsReportData)
        {
            if(productsReportData == null)
            {
                throw new ArgumentNullException("productsReportData");
            }

            ProductReport report = new ProductReport();
            report.Name = productsReportData.First().VendorName;
            report.FileStream = ReportDocumentGenerator.GenerateReportDocument(productsReportData);

            return report;
        }
    }
}
