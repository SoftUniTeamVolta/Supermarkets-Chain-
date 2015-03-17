namespace SupermarketChain.Data.Models.SQLServerModels
{
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class SuperMarket : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}