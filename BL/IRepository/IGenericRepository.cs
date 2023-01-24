using System.Linq.Expressions;

namespace WebApplication1.BL.IRepository
{
    public interface IGenericRepository<TEntity>where TEntity : class
    {
        Task<IList<TEntity>>GetAllAsync(
            Expression<Func<TEntity,bool>>expression=null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>orderBy=null,
            List<string>includes=null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null,List<string> includes = null);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(int id);
        void DeleteRangeAsync(IEnumerable<TEntity> entities);
        void UpdateAsync(TEntity entity);
    }
}
