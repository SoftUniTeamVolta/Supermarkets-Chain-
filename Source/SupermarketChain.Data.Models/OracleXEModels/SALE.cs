namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts.Interfaces;
    using System;


    [Table("ADMIN.SALES")]
    public class SALE : IDeletableEntity, IAuditInfo
    {
        private decimal sum;

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("PRODUCT_ID")]
        public int ProductId { get; set; }

        public virtual PRODUCT Product { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [Column("QUANTITY")]
        public int Quantity { get; set; }

        [Required]
        [Column("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        [Column("SUM")]
        public decimal Sum
        {
            get { return this.sum; }
            private set { this.sum = (this.Quantity*this.UnitPrice); }
        }

        [Required]
        [Column("SUPERMARKET_ID")]
        public int SuperMarketId { get; set; }

        public virtual SUPERMARKET SuperMarket { get; set; }

        [Column("IS_DELETED")]
        public bool IsDeleted { get; set; }

        [Column("DELETED_ON")]
        public DateTime? DeletedOn { get; set; }

        [Column("CREATED_ON")]
        public DateTime CreatedOn { get; set; }

        [Column("MODIFIED_ON")]
        public DateTime? ModifiedOn { get; set; }

        [Column("PRESERVE_CREATED_ON")]
        public bool PreserveCreatedOn { get; set; }

        [ForeignKey("Vendor")]
        [Column("VENDOR_ID")]
        public int VendorId { get; set; }

        public virtual VENDOR Vendor { get; set; }
    }
}