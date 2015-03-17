namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;
    using Contracts.Interfaces;

    [Table("ADMIN.VENDORS")]
    public class VENDOR : IDeletableEntity, IAuditInfo
    {
        private ICollection<PRODUCT> products;
        private ICollection<SALE> sales; 

        public VENDOR()
        {
            this.products = new HashSet<PRODUCT>();
            this.sales = new HashSet<SALE>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        [Column("NAME")]
        public string Name { get; set; }

        public virtual ICollection<PRODUCT> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public virtual ICollection<SALE> Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        }

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
