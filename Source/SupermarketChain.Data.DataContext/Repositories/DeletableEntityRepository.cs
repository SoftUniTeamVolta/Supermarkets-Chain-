namespace SupermarketChain.Data.DataContext.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Interfaces;

    public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T>, IAuditInfoEntityRepository<T>
        where T : class, IDeletableEntity, IAuditInfo
    {
        public DeletableEntityRepository(IDataContext context)
            : base(context)
        {

        }

        public override IEnumerable<T> GetAll()
        {
            return base.GetAll().Where(e => !e.IsDeleted);
        }

        public IEnumerable<T> AllWithDeleted()
        {
            return base.GetAll();
        }
    }
}
