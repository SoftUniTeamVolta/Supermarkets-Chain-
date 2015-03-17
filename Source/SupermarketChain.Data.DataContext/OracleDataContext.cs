namespace SupermarketChain.Data.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Contracts.Interfaces;
    using Models.OracleXEModels;

    public class OracleDataContext : DbContext
    {
        public OracleDataContext()
            : base("name=OracleDb")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<OracleDataContext, Migrations.Oracle.Configuration>());
        }

        public IDbSet<SUPERMARKET> Supermarkets { get; set; }

        public IDbSet<SALE> Sales { get; set; }

        public IDbSet<PRODUCT> Products { get; set; }

        public IDbSet<VENDOR> Vendors { get; set; }

        public IDbSet<MEASURE> Measures { get; set; }

        public static OracleDataContext Create()
        {
            return new OracleDataContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ADMIN");
        }

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
                var entity = (IAuditInfo)entry.Entity;

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
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}