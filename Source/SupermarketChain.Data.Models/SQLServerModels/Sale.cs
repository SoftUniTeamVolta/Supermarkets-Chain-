namespace SupermarketChain.Data.Models.SQLServerModels
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using Contracts.Interfaces;

    public class Sale : IDeletableEntity
    {
        private decimal sum;

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

        public decimal Sum
        {
            get { return this.sum; }
            private set { this.sum = (this.Quantity * this.UnitPrice); }
        }

        [Required]
        public int SuperMarketId { get; set; }

        public SuperMarket SuperMarket { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}