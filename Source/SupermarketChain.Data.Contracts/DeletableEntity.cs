namespace SupermarketChain.Data.Contracts
{
    using System;

    using Interfaces;

    public class DeletableEntity : AuditInfo, IDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
