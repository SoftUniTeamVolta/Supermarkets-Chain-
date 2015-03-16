namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using Contracts.Interfaces;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ADMIN.SUPERMARKETS")]
    public class SUPERMARKET : IDeletableEntity
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string NAME { get; set; }

        [Column("IS_DELETED")]
        public bool IsDeleted { get; set; }

        [Column("DELETED_ON")]
        public DateTime? DeletedOn { get; set; }
    }
}