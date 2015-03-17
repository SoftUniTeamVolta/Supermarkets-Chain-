﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SupermarketChain.Data.Models.SQLServerModels
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using Contracts;

    public class Sale : DeletableEntity
    {
        public Sale()
        {
            this.Sum = this.Quantity*this.UnitPrice;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal Sum { get; private set; }

        public SuperMarket SuperMarket { get; set; }

        public Vendor Vendor { get; set; }
    }
}