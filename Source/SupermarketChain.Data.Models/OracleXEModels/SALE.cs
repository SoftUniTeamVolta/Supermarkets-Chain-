namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Contracts.Interfaces;

    [Table("ADMIN.SALES")]
    public class SALE : IDeletableEntity, IAuditInfo
    {
        public SALE()
        {
            this.Sum = this.Quantity*this.UnitPrice;
            this.Date = DateTime.Now;
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("DATE")]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey("Product")]
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
        public decimal Sum { get; private set; }

        [Required]
        [ForeignKey("Supermarket")]
        [Column("SUPERMARKET_ID")]
        public int SupermarketId { get; set; }

        public virtual SUPERMARKET Supermarket { get; set; }

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