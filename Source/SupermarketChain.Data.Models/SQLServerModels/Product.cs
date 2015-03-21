using System.Collections;
using System.Diagnostics;

namespace SupermarketChain.Data.Models.SQLServerModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;
    using System.Collections.Generic;

    public class Product : DeletableEntity
    {
        private ICollection<Sale> sales;

        public Product()
        {
            this.sales = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("Vendor")]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Name { get; set; }

        [ForeignKey("Measure")]
        public int MeasureId { get; set; }

        public virtual Measure Measure { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
