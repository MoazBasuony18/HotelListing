using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication1.BL.IRepository;
using WebApplication1.Data;
using WebApplication1.Models;
using X.PagedList;

namespace WebApplication1.BL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly HotelDbContext context;
        private readonly DbSet<TEntity> db;

        public GenericRepository(HotelDbContext context)
        {
            this.context = context;
            db=context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await db.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await db.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await db.FindAsync(id);
            db.Remove(entity);
        }

        public void DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            db.RemoveRange(entities);
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<string> includes = null)
        {
            IQueryable<TEntity> query = db;
            if (expression != null)
            {
                query=query.Where(expression);
            }
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }
            if(orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IPagedList<TEntity>> GetAllAsync(RequestPramas requestPramas,List<string> includes = null)
        {
            IQueryable<TEntity> query = db;
            
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }
            return await query.AsNoTracking().ToPagedListAsync(requestPramas.PageNumber,requestPramas.PageSize);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression = null, List<string> includes = null)
        {
            IQueryable<TEntity> query = db;
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void UpdateAsync(TEntity entity)
        {
            db.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
