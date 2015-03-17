﻿namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    [Table("ADMIN.MEASURES")]
    public class MEASURE : DeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string NAME { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string ABBREVIATION { get; set; }
    }
}