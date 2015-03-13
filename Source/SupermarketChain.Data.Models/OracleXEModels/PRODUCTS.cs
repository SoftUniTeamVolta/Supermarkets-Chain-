﻿namespace SupermarketChain.Data.Models.OracleXEModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Contracts;

    public class PRODUCTS : DeletableEntity
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("VENDOR")]
        public int VENDOR_ID { get; set; }

        public virtual VENDORS VENDOR { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string NAME { get; set; }

        [ForeignKey("MEASURE")]
        public int MEASURE_ID { get; set; }

        public virtual MEASURES MEASURE { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PRICE { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DATE { get; set; }
    }
}
