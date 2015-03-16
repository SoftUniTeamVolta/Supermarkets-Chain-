namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    [Table("ADMIN.VENDORS")]
    public class VENDOR : DeletableEntity
    {
        private ICollection<PRODUCT> products;

        public VENDOR()
        {
            this.products = new HashSet<PRODUCT>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string NAME { get; set; }

        public virtual ICollection<PRODUCT> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
