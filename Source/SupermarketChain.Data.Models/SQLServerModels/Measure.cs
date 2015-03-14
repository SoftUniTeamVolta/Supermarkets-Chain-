namespace SupermarketChain.Data.Models.SQLServerModels
{
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class Measure : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string Abbreviation { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
