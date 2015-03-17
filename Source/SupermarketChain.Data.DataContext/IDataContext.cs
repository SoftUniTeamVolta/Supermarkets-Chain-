namespace SupermarketChain.Data.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;
    using Models.SQLServerModels;

    public interface IDataContext : IDisposable
    {
        IDbSet<SuperMarket> SuperMarkets { get; set; }

        IDbSet<Sale> Sales { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<Vendor> Vendors { get; set; }

        IDbSet<Measure> Measures { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
