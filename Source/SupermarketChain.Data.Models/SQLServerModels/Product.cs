namespace SupermarketChain.Data.Models.SQLServerModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    public class Product : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Vendor")]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Name { get; set; }

        [ForeignKey("Measure")]
        public int MeasureId { get; set; }

        public virtual Measure Measure { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
