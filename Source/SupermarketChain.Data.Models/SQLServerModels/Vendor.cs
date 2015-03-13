namespace SupermarketChain.Data.Models.SQLServerModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class Vendor : DeletableEntity
    {
        private ICollection<Product> products;

        public Vendor()
        {
            this.products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Name { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }
    }
}
