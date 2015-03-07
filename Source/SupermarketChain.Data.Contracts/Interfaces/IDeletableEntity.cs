namespace SupermarketChain.Data.Contracts.Interfaces
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public interface IDeletableEntity
    {
        [Editable(false)]
        [DataType(DataType.DateTime)]
        bool IsDeleted { get; set; }

        [Editable(false)]
        DateTime? DeletedOn { get; set; }
    }
}
