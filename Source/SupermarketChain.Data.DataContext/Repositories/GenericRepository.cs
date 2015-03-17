namespace SupermarketChain.Data.DataContext.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Contracts.Interfaces;
    using Models.OracleXEModels;

    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private IDataContext msContext;
        private OracleDataContext oracleContext;
        private IDbSet<T> dbSet; 

        public GenericRepository(IDataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.msContext = context;
            this.dbSet = this.MsContext.Set<T>();
        }

        public GenericRepository(OracleDataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.oracleContext = context;
            this.dbSet = this.OracleContext.Set<T>();
        }

        protected IDbSet<T> DbSet 
        {
            get { return this.dbSet; }
        }

        protected IDataContext MsContext 
        {
            get { return this.msContext; }
        }

        protected OracleDataContext OracleContext
        {
            get { return this.oracleContext; }
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.DbSet.ToList();
        }

        public virtual IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return this.DbSet.Where(expression).ToList();
        }

        public virtual T GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            dynamic context;
            if (entity is VENDOR || entity is MEASURE || entity is PRODUCT)
            {
                context = this.OracleContext;
            }
            else
            {
                context = this.MsContext;
            }

            DbEntityEntry entry = context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            } 
        }

        public virtual void Update(T entity)
        {
            dynamic context;
            if (entity is VENDOR || entity is MEASURE || entity is PRODUCT)
            {
                context = this.OracleContext;
            }
            else
            {
                context = this.MsContext;
            }

            DbEntityEntry entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dynamic context;
            if (entity is VENDOR || entity is MEASURE || entity is PRODUCT)
            {
                context = this.OracleContext;
            }
            else
            {
                context = this.MsContext;
            }

            DbEntityEntry entry = context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual void Detach(T entity)
        {
            dynamic context;
            if (entity is VENDOR || entity is MEASURE || entity is PRODUCT)
            {
                context = this.OracleContext;
            }
            else
            {
                context = this.MsContext;
            }

            DbEntityEntry entry = context.Entry(entity);
            entry.State = EntityState.Detached;
        }

        public int SaveChanges()
        {
            if (this.OracleContext == null)
            {
                return this.MsContext.SaveChanges();
            }

            return this.OracleContext.SaveChanges();
        }

        public void Dispose()
        {
            if (this.OracleContext == null)
            {
                this.MsContext.Dispose();
            }

            this.OracleContext.Dispose();
        }

        public virtual T GetFirstEntry()
        {
            return this.DbSet.ToList().FirstOrDefault();
        }

        public virtual T GetLatestEntry()
        {
            return this.DbSet.ToList().LastOrDefault();
        }
    }
}
