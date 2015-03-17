namespace SupermarketChain.Data.Contracts
{
    using System;

    using Interfaces;

    public abstract class DeletableEntity : AuditInfo, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
