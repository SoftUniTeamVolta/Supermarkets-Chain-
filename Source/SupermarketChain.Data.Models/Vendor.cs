namespace SupermarketChain.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SupermarketChain.Data.Contracts;

    public class Vendor : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Name { get; set; }
    }
}
