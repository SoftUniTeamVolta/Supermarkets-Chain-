﻿namespace SupermarketChain.Data.Models.SQLServerModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Contracts;

    public class Vendor : DeletableEntity
    {
        private ICollection<Product> products;
        private ICollection<Sale> sales;
        private ICollection<Expense> exprenses;

        public Vendor()
        {
            this.products = new HashSet<Product>();
            this.sales = new HashSet<Sale>();
            this.exprenses = new HashSet<Expense>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Name { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get { return this.sales; }
            set { this.sales = value; }
        }

        public virtual ICollection<Product> Products
        {
            get { return this.products; }
            set { this.products = value; }
        }

        public virtual ICollection<Expense> Exprenses
        {
            get { return this.exprenses; }
            set { this.exprenses = value; }
        }
    }
}
