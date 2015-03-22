namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Contracts.Interfaces;
    using System.ComponentModel;

    [Table("ADMIN.SUPERMARKETS")]
    public class SUPERMARKET : IDeletableEntity, IAuditInfo
    {
        private DateTime createdOn;

        private const bool PRESERVE_CREATED_ON = true;
        private bool preserveCreatedOn = PRESERVE_CREATED_ON;

        public SUPERMARKET()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Column("NAME")]
        public string Name { get; set; }

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
        [DefaultValue(SUPERMARKET.PRESERVE_CREATED_ON)]
        public bool PreserveCreatedOn
        {
            get { return this.preserveCreatedOn; }
            set { this.preserveCreatedOn = value; }
        }
    }
}