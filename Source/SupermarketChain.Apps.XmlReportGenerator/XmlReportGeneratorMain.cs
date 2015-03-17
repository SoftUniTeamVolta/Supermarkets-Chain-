using System.Data.Entity;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using SupermarketChain.Data.DataContext;

namespace SupermarketChain.Apps.XmlReportGenerator
{
    using SupermarketChain.Common.Utils;
    using SupermarketChain.Data.Models.SQLServerModels;
    using System;
    using System.Collections.Generic;

    public class XmlReportGeneratorMain
    {
        private static readonly string DocumentName = "SalesReport.xml";
        private static readonly string DocumentOutputPath;

        static XmlReportGeneratorMain()
        {
            string outputDir = ConfigUtils.GetAppSetting("OutputDir");
            if (string.IsNullOrEmpty(outputDir))
            {
                outputDir = "./";
            }

            XmlReportGeneratorMain.DocumentOutputPath = outputDir + XmlReportGeneratorMain.DocumentName;
        }

        public static void Main(string[] args)
        {
            DateTime startDate = new DateTime(1300, 01, 01);
            DateTime endDate = new DateTime(2040, 01, 01);

            GenerateXmlReport(startDate, endDate);
            Console.WriteLine("Xml report has been generated!");
        }

        private static void GenerateXmlReport(DateTime startDate, DateTime endDate)
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
                doc.Save("../../../generated-files/xml-reports.xml");
            }
        }

        private static void ValidateInputArguments(string[] args, out DateTime startDate, out DateTime endDate)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Arguments list is empty");
            }
            if (!DateTime.TryParse(args[0], out startDate))
            {
                throw new FormatException("Start Date is not in valid format");
            }
            if (!DateTime.TryParse(args[1], out endDate))
            {
                throw new FormatException("End Date is not in valid format");
            }
        }
    }
}
