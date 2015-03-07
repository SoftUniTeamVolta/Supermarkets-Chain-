namespace SupermarketChain.Data.DataContext
{
    using System.Data.Entity;

    using Migrations;
    using Models;

    public class DataContext : DbContext, IDataContext
    {
        public DataContext()
            : base("SupermarketChain")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
        }


        public DbSet<Product> Products { get; set; }

        public static DataContext Create()
        {
            return new DataContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
