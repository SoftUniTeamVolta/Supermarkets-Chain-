using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace SupermarketChain.Data.Models.SQLServerModels
{
    public class Exprense
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
    }
}