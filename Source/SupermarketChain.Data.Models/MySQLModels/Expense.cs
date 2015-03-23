namespace SupermarketChain.Data.Models.MySQLModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Expense
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal")]
        public decimal Sum { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}
