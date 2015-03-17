namespace SupermarketChain.Data.Models.SQLServerModels
{
	using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using System;
    using Contracts;

    public class Sale : DeletableEntity
    {
        public Sale()
        {
            this.Sum = this.Quantity*this.UnitPrice;
            this.Date = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Column("Sum")]
        public decimal Sum { get; set; }

        public SuperMarket SuperMarket { get; set; }

        public Vendor Vendor { get; set; }
    }
}