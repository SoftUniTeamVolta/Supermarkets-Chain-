namespace SupermarketChain.Data.DataContext
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Contracts.Interfaces;
    using MySql.Data.Entity;
    using SupermarketChain.Data.Models.MySQLModels;
    using SupermarketChain.Data.DataContext.Migrations.MySQL;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySQLContext : DbContext
    {
        public MySQLContext()
            : base("MySqlDbContext")
        {
            Database.SetInitializer<MySQLContext>(new MigrateDatabaseToLatestVersion<MySQLContext, Configuration>());
        }


        public IDbSet<Expense> Expenses { get; set; }

        public IDbSet<Income> Incomes { get; set; }

        public IDbSet<Measure> Measures { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Vendor> Vendors { get; set; }


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
