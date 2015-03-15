namespace SupermarketChain.Apps.PdfReportGenerator
{
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using SupermarketChain.Common.Utils;
    using SupermarketChain.Data.Models.SQLServerModels;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class PdfGeneratorMain
    {
        private static readonly string PdfDocumentName = "SalesReport.pdf";
        private static readonly string DocumentOutputPath;

        static PdfGeneratorMain()
        {
            string outputDir = ConfigUtils.GetAppSetting("OutputDir");
            if(string.IsNullOrEmpty(outputDir))
            {
                outputDir = "./"; 
            }

            PdfGeneratorMain.DocumentOutputPath = outputDir + PdfGeneratorMain.PdfDocumentName;
        }

        public static void Main(string[] args)
        {
            DateTime startDate;
            DateTime endDate;

            PdfGeneratorMain.ValidateInputArguments(args, out startDate, out endDate);

            ICollection<Product> data = new List<Product>();

            Document pdfReport = PdfGeneratorMain.GeneratePdfReport(data, startDate, endDate);
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

        private static Document GeneratePdfReport(ICollection<Product> data, DateTime startDate, DateTime endDate)
        {
            Document pdf = new Document();
            PdfWriter writer = PdfWriter.GetInstance(pdf, new FileStream(PdfGeneratorMain.DocumentOutputPath, FileMode.Create));
            pdf.Open();

            // do some work

            pdf.Close();
            return pdf;
        }
    }
}
