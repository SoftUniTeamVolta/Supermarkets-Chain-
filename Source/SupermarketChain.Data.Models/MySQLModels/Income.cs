namespace SupermarketChain.Data.Models.MySQLModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Income
    {

        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int VendorId { get; set; }

        [Required]
        public string VendorName { get; set; }

        public int TotalQuantity { get; set; }

        [Column(TypeName = "decimal")]
        public decimal TotalIncome { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }

        public virtual Product Product { get; set; }

        public virtual Vendor Vendor { get; set; }

    }
}
