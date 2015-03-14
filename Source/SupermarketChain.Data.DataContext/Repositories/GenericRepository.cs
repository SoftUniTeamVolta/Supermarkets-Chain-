namespace SupermarketChain.Data.DataContext.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Contracts.Interfaces;

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
            DbEntityEntry entry = this.MsContext.Entry(entity);
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
            DbEntityEntry entry = this.MsContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry entry = this.MsContext.Entry(entity);
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
            DbEntityEntry entry = this.MsContext.Entry(entity);

            entry.State = EntityState.Detached;
        }

        public int SaveChanges()
        {
            return this.MsContext.SaveChanges();
        }

        public void Dispose()
        {
            this.MsContext.Dispose();
        }
    }
}
