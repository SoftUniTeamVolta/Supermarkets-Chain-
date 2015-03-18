using SupermarketChain.Data.Models.SQLServerModels;

namespace SupermarketChain.Apps.XmlExpenseImport
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Data.DataContext;

    class Program
    {
        static void Main(string[] args)
        {
            ImportExpenses("../../../../files-for-import/Sample-Vendor-Expenses.xml");

            Console.WriteLine("Expenses has been added to DB.");
        }

        private static void ImportExpenses(string path)
        {
            XDocument doc = new XDocument();

            try
            {
                doc = XDocument.Load(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found! See trace: " + e.StackTrace);
            }

            var expensesByVendor =
                (from vendor in doc.Descendants("vendor")
                 select new
                 {
                     Name = vendor.Attribute("name").Value,
                     Expenses = vendor
                         .Elements("expenses")
                         .Select(e =>
                             new
                             {
                                 Month = DateTime.Parse(e.Attribute("month").Value),
                                 Sum = decimal.Parse(e.Value)
                             })
                         .ToList()
                 })
                    .ToList();

            foreach (var vendor in expensesByVendor)
            {
                foreach (var expense in vendor.Expenses)
                {
                    AddExpense(vendor.Name, expense.Month, expense.Sum);
                }
            }
        }

        private static void AddExpense(string vendorName, DateTime expenseDate, decimal expenseSum)
        {
            using (var context = new MsDataContext())
            {
                int vendorId = 0;

                if (context.Vendors.Any(v => v.Name == vendorName))
                {
                    vendorId = context.Vendors.FirstOrDefault(v => v.Name == vendorName).Id;
                }
                else
                {
                    var newVendor = new Vendor {Name = vendorName};

                    context.Vendors.Add(newVendor);
                    context.SaveChanges();

                    vendorId = newVendor.Id;
                }

                context.Expenses.Add(new Expense {VendorId = vendorId, Date = expenseDate, Sum = expenseSum});
                context.SaveChanges();
            }
        }
    }
}
