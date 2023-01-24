using WebApplication1.BL.IRepository;
using WebApplication1.Data;

namespace WebApplication1.BL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelDbContext context;
        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;

        public UnitOfWork(HotelDbContext context)
        {
            this.context = context;
        }
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(context);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(context);

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
