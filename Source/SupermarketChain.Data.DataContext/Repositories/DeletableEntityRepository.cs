namespace SupermarketChain.Data.DataContext.Repositories
{
    using System.Linq;

    using SupermarketChain.Data.Contracts.Interfaces;

    public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T>, IAuditInfoEntityRepository<T>
        where T : class, IDeletableEntity, IAuditInfo
    {
        public DeletableEntityRepository(IDataContext context)
            : base(context)
        {

        }

        public override IQueryable<T> All()
        {
            return base.All().Where(e => !e.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }
    }
}
