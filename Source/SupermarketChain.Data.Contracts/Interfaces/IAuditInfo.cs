namespace SupermarketChain.Data.Contracts.Interfaces
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public interface IAuditInfo
    {
        [Editable(false)]
        [DataType(DataType.DateTime)]
        DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        DateTime? ModifiedOn { get; set; }

        [NotMapped]
        bool PreserveCreatedOn { get; set; }
    }
}
