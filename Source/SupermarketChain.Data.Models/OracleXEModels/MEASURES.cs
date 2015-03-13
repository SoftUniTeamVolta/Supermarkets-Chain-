namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class MEASURES : DeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string NAME { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string ABBREVIATION { get; set; }
    }
}
