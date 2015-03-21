namespace SupermarketChain.Apps.XmlReportGenerator
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Xml;
    using Common.Utils;
    using Data.DataContext;

    public static class XmlReportGenerator
    {
        private const string DocumentName = "SalesReport.xml";
        private const string OutputDir = "../../../generated-files";

        public static void GenerateXmlReport(DateTime startDate, DateTime endDate)
        {
            using (var context = new MsDataContext())
            {
                var doc = new XmlDocument();

                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", null, null);
                doc.AppendChild(declaration);

                XmlElement rootNode = doc.CreateElement("sales");

                var vendors = context
                    .Vendors
                    .Where(v =>
                        v.Sales
                            .Any(s => s.Date >= startDate && s.Date <= endDate))
                    .Include(v => v.Sales);

                foreach (var vendor in vendors)
                {
                    XmlElement currVendorNode = doc.CreateElement("sale");
                    currVendorNode.SetAttribute("vendor", vendor.Name);

                    foreach (var sale in vendor.Sales)
                    {
                        XmlElement currSaleNode = doc.CreateElement("summary");
                        currSaleNode.SetAttribute("date", sale.Date.ToString("dd-MMM-yyyy"));
                        currSaleNode.SetAttribute("total-sum", sale.Sum.ToString());

                        currVendorNode.AppendChild(currSaleNode);
                    }

                    rootNode.AppendChild(currVendorNode);
                }

                doc.AppendChild(rootNode);
                doc.Save(OutputDir + "/" + DocumentName);
            }

            Console.WriteLine("Xml report has been generated!");
            Console.WriteLine();
        }
    }
}