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
            if(string.IsNullOrEmpty(outputDir))
            {
                outputDir = "./"; 
            }

            XmlReportGeneratorMain.DocumentOutputPath = outputDir + XmlReportGeneratorMain.DocumentName;
        }

        public static void Main(string[] args)
        {
            DateTime startDate;
            DateTime endDate;

            ValidateInputArguments(args, out startDate, out endDate);

            ICollection<Product> data = new List<Product>();

            GenerateXmlReport(data, startDate, endDate);
        }

        private static void GenerateXmlReport(ICollection<Product> data, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
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
