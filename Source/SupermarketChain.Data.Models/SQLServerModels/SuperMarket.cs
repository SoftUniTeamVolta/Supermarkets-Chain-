namespace SupermarketChain.Data.Models.SQLServerModels
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using Contracts.Interfaces;

    public class SuperMarket : IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}