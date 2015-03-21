namespace SupermarketChain.Data.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Contracts.Interfaces;
    using Models.SQLServerModels;

    using Migrations.SQLServer;

    public class MsDataContext : DbContext, IDataContext
    {
        public MsDataContext()
            : base("SupermarketChain")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MsDataContext, Configuration>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<MsDataContext>());
        }

        public IDbSet<Supermarket> SuperMarkets { get; set; }

        public IDbSet<Sale> Sales { get; set; }

        public IDbSet<Expense> Expenses { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Vendor> Vendors { get; set; }

        public IDbSet<Measure> Measures { get; set; }

        public static MsDataContext Create()
        {
            return new MsDataContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDefaultSchema("SupermarketChain");
        //}

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();

            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                ChangeTracker.Entries()
                             .Where(
                                 e =>
                                     e.Entity is IAuditInfo &&
                                     ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo) entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    ChangeTracker.Entries()
                                 .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity) entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}