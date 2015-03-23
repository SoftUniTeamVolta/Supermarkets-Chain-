namespace SupermarketChain.Apps.PdfReportGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Data.DataContext;
    using Data.DataContext.Repositories;
    using Data.Models.SQLServerModels;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public static class PdfGeneratorMain
    {
        private static readonly string PdfDocumentName = "SalesReport.pdf";
        private static readonly string DocumentOutputPath;
        private const string OutputDir = "../../../generated-reports/pdf-reports/";

        static PdfGeneratorMain()
        {
            var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            var path = Path.GetDirectoryName(assembly.Location);

            PdfGeneratorMain.DocumentOutputPath = Path.Combine(path, (PdfGeneratorMain.OutputDir + PdfGeneratorMain.PdfDocumentName));
           
        }

        public static void Main(string[] args)
        {
            // Initialize only for testing! Remove when done.
            DateTime startDate; //= new DateTime(2015, 03, 16);
            DateTime endDate;// = new DateTime(2015, 03, 25);



            try
            {
                using (var context = new MsDataContext())
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(PdfGeneratorMain.DocumentOutputPath));
                    PdfGeneratorMain.ValidateInputArguments(args, out startDate, out endDate);
                    SortedDictionary<string, ICollection<Sale>> data = PdfGeneratorMain.FetchDataFromDatabase(
                        context, startDate, endDate);
                    Document pdfReport = PdfGeneratorMain.GeneratePdfReport(data, startDate, endDate);
                    Console.WriteLine("Pdf sales report was generated");
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file you tried to write or modified is unaccessible right now. Please try again.");
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
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

        private static SortedDictionary<string, ICollection<Sale>> FetchDataFromDatabase(IDataContext context,
                                                                                           DateTime startDate,
                                                                                           DateTime endDate)
        {
            var sales = new GenericRepository<Sale>(context);
            var result =
                sales.Find(
                    s => DbFunctions.TruncateTime(s.Date) >= startDate && DbFunctions.TruncateTime(s.Date) <= endDate)
                     .ToList();

            var sortedData = new SortedDictionary<string, ICollection<Sale>>();

            foreach (var sale in result)
            {
                string date = sale.Date.ToShortDateString();
                if (!sortedData.ContainsKey(date))
                {
                    sortedData.Add(date, new List<Sale>());
                }
                else
                {
                    sortedData[date].Add(sale);
                }
            }

            return sortedData;
        }

        private static Document GeneratePdfReport(SortedDictionary<string, ICollection<Sale>> saleData,
                                                  DateTime startDate, DateTime endDate)
        {
            var gray = new BaseColor(175, 175, 175);
            var darkGrey = new BaseColor(200, 200, 200);
            Document pdf = new Document(PageSize.A4, 2, 2, 2, 2);
            PdfPTable table = new PdfPTable(5); // 5-number of columns
            table.HorizontalAlignment = 1; // 0-left, 1-center, 2-right
            table.SpacingBefore = 20f;
            table.SpacingAfter = 20f;
            //table.HeaderRows = 1;
            using (
                PdfWriter writer = PdfWriter.GetInstance(pdf,
                    new FileStream(PdfGeneratorMain.DocumentOutputPath, FileMode.Create)))
            {
                PdfPCell headLineCell = new PdfPCell(new Phrase("Aggregated Sales Report"));
                headLineCell.Colspan = 5;
                headLineCell.HorizontalAlignment = 1;
                table.DefaultCell.HorizontalAlignment = 1;
                table.AddCell(headLineCell);
                
                foreach (var date in saleData)
                {
                    string dateStr = date.Key;
                    decimal totalSum = 0m;
                    var dateRow = new PdfPCell(new Phrase("Date: " + dateStr));
                    dateRow.Colspan = 5;
                    dateRow.BackgroundColor = gray;
                    table.AddCell(dateRow);
                    Chunk c1 = new Chunk("Product", new Font(Font.FontFamily.HELVETICA, Font.BOLD));
                    Chunk c2 = new Chunk("Quantity", new Font(Font.FontFamily.HELVETICA, Font.BOLD));
                    Chunk c3 = new Chunk("Unit Price", new Font(Font.FontFamily.HELVETICA, Font.BOLD));
                    Chunk c4 = new Chunk("Location", new Font(Font.FontFamily.HELVETICA, Font.BOLD));
                    Chunk c5 = new Chunk("Sum", new Font(Font.FontFamily.HELVETICA, Font.BOLD));

                    var p1 = new PdfPCell(new Phrase(c1));
                    var p2 = new PdfPCell(new Phrase(c2));
                    var p3 = new PdfPCell(new Phrase(c3));
                    var p4 = new PdfPCell(new Phrase(c4));
                    var p5 = new PdfPCell(new Phrase(c5));

                    p1.BackgroundColor = darkGrey;
                    p2.BackgroundColor = darkGrey;
                    p3.BackgroundColor = darkGrey;
                    p4.BackgroundColor = darkGrey;
                    p5.BackgroundColor = darkGrey;

                    table.AddCell(p1);
                    table.AddCell(p2);
                    table.AddCell(p3);
                    table.AddCell(p4);
                    table.AddCell(p5);
                    //table.getDefaultCell().setBackgroundColor(BaseColor.LIGHT_GRAY);
                    foreach (var sale in date.Value)
                    {
                        table.AddCell(sale.Product.Name);
                        table.AddCell(sale.Quantity.ToString());
                        table.AddCell(sale.UnitPrice.ToString());
                        table.AddCell(sale.Supermarket.Name);
                        table.AddCell(sale.Sum.ToString());
                        totalSum += sale.Sum;
                    }
                    var totalSumCell = new PdfPCell(new Phrase("Total sum for " + dateStr + ":"));
                    totalSumCell.Colspan = 4;
                    totalSumCell.HorizontalAlignment = 2;
                    table.AddCell(totalSumCell);
                    table.AddCell(totalSum.ToString());
                }
                
                pdf.Open();
                pdf.Add(table);
                pdf.Close();
            }

            return pdf;
        }
    }
}