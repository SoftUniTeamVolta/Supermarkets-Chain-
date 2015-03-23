namespace SupermarketChain.Apps.XmlReportGenerator
{
    using System;
    using System.Data.Entity;
    using System.Data.SqlTypes;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Common.Utils;
    using Data.DataContext;

    public static class XmlReportGenerator
    {
        private const string DocumentName = "SalesReport.xml";
        private const string OutputDir = "../../../generated-reports/xml-reports/";

        public static void GenerateXmlReport(string startDateStr, string endDateStr)
        {
            DateTime startDate;
            DateTime endDate;

            using (var context = new MsDataContext())
            {
                try
                {
                    Directory.CreateDirectory(XmlReportGenerator.OutputDir);
                    XmlReportGenerator.ValidateInputArguments(startDateStr, endDateStr, out startDate, out endDate);

                    var doc = new XmlDocument();
                    XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", null, null);
                    doc.AppendChild(declaration);

                    XmlElement rootNode = doc.CreateElement("sales");

                    var vendors = context
                        .Vendors
                        .Where(v =>
                            v.Sales
                             .Any(s => DbFunctions.TruncateTime(s.Date) >= startDate && DbFunctions.TruncateTime(s.Date) <= endDate))
                        .Include(v => v.Sales);

                //    var result =
                //context.Vendors.Where(
                //    s => vendors. DbFunctions.TruncateTime(s.Date) >= startDate && DbFunctions.TruncateTime(s.Date) <= endDate)
                //     .ToList();

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
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (IOException e)
                {

                    Console.WriteLine(e.Message);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Xml report has been generated!");
            Console.WriteLine();
        }

        private static void ValidateInputArguments(string startDateStr, string endDateStr, out DateTime startDate, out DateTime endDate)
        {

            if (!DateTime.TryParse(startDateStr, out startDate))
            {
                throw new FormatException("Start Date is not in valid format");
            }
            if (!DateTime.TryParse(endDateStr, out endDate))
            {
                throw new FormatException("End Date is not in valid format");
            }
        }
    }
}