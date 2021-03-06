﻿namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Contracts.Interfaces;
    using System.Collections.Generic;
    using System.ComponentModel;

    [Table("ADMIN.PRODUCTS")]
    public class PRODUCT : IDeletableEntity, IAuditInfo
    {
        private DateTime createdOn;

        private const bool PRESERVE_CREATED_ON = true;
        private bool preserveCreatedOn = PRESERVE_CREATED_ON;

        private ICollection<SALE> sales;

        public PRODUCT()
        {
            this.sales = new HashSet<SALE>();
            this.CreatedOn = DateTime.Now;
        }

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

        public virtual ICollection<SALE> Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        }

        [Required]
        [DataType(DataType.Currency)]
        [Column("PRICE")]
        public decimal Price { get; set; }

        [Column("IS_DELETED")]
        public bool IsDeleted { get; set; }

        [Column("DELETED_ON")]
        public DateTime? DeletedOn { get; set; }

        [Column("CREATED_ON")]
        public DateTime CreatedOn
        {
            get { return this.createdOn; }
            set { this.createdOn = value; }
        }

        [Column("MODIFIED_ON")]
        public DateTime? ModifiedOn { get; set; }

        [Column("PRESERVE_CREATED_ON")]
        [DefaultValue(PRODUCT.PRESERVE_CREATED_ON)]
        public bool PreserveCreatedOn
        {
            get { return this.preserveCreatedOn; }
            set { this.preserveCreatedOn = value; }
        }
    }
}