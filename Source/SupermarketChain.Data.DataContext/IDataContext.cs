namespace SupermarketChain.Data.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface IDataContext : IDisposable
    {
        DbSet<Product> Products { get; set; }

        int SaveChanges();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        IDbSet<T> Set<T>() where T : class;
    }
}
