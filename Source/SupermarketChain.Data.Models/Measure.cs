namespace SupermarketChain.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SupermarketChain.Data.Contracts;

    public class Measure : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string Abbreviation { get; set; }
    }
}
