namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;
    using Contracts.Interfaces;

    [Table("ADMIN.PRODUCTS")]
    public class PRODUCT : IDeletableEntity, IAuditInfo
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [ForeignKey("Vendor")]
        [Column("VENDOR_ID")]
        public int VendorId { get; set; }

        public virtual VENDOR Vendor { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5)]
        [Column("NAME")]
        public string Name { get; set; }

        [ForeignKey("Measure")]
        [Column("MEASURE_ID")]
        public int MeasureId { get; set; }

        public virtual MEASURE Measure { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column("PRICE")]
        public decimal Price { get; set; }

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
    }
}