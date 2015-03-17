namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts.Interfaces;
    using System;
    using SQLServerModels;

    [Table("ADMIN.SALES")]
    public class SALE : IDeletableEntity
    {
        private decimal sum;

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

        public decimal Sum
        {
            get { return this.sum; }
            private set { this.sum = (this.Quantity*this.UnitPrice); }
        }

        [Required]
        [Column("SUPER_MARKET_ID")]
        public int SuperMarketId { get; set; }

        public SuperMarket SuperMarket { get; set; }

        [Column("IS_DELETED")]
        public bool IsDeleted { get; set; }

        [Column("DELETED_ON")]
        public DateTime? DeletedOn { get; set; }
    }
}