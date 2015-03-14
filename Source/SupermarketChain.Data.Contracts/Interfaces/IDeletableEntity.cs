namespace SupermarketChain.Data.Contracts.Interfaces
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public interface IDeletableEntity
    {
        [Editable(false)]
        bool IsDeleted { get; set; }

        [Editable(false)]
        [DataType(DataType.DateTime)]
        DateTime? DeletedOn { get; set; }
    }
}
