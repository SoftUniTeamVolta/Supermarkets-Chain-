namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;
    using Contracts.Interfaces;

    [Table("ADMIN.SUPERMARKETS")]
    public class SUPERMARKET : IDeletableEntity, IAuditInfo
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Column("Name")]
        public string Name { get; set; }

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