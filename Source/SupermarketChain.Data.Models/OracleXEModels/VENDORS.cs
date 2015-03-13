namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class VENDORS : DeletableEntity
    {
        private ICollection<PRODUCTS> products;

        public VENDORS()
        {
            this.products = new HashSet<PRODUCTS>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string NAME { get; set; }

        public virtual ICollection<PRODUCTS> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
