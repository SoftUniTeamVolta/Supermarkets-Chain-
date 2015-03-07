namespace SupermarketChain.Data.Contracts
{
    using System;

    using Interfaces;

    public class AuditInfo : IAuditInfo
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }
    }
}
