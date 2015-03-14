namespace SupermarketChain.Data.Contracts.Interfaces
{
    using System.Collections.Generic;

    public interface IDeletableEntityRepository<T> : IRepository<T> where T : class
    {
        IEnumerable<T> AllWithDeleted();
    }
}