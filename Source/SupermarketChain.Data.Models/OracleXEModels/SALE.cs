namespace SupermarketChain.Data.Models.OracleXEModels
{
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts.Interfaces;
    using System;
    using SQLServerModels;

    [Table("ADMIN.SALES")]
    public class SALE : IDeletableEntity
    {
        public SALE()
        {
            this.Sum = this.Quantity*this.UnitPrice;
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("PRODUCT_ID")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Column("QUANTITY")]
        public int Quantity { get; set; }

        [Required]
        [Column("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        public decimal Sum { get; private set; }

        [Required]
        [Column("SUPER_MARKET_ID")]
        public int SuperMarketId { get; set; }

        public SUPERMARKET SuperMarket { get; set; }

        [Column("IS_DELETED")]
        public bool IsDeleted { get; set; }

        [Column("DELETED_ON")]
        public DateTime? DeletedOn { get; set; }

        [Column("VENDOR_ID")]
        public int VendorId { get; set; }

        public VENDOR Vendor { get; set; }
    }
}