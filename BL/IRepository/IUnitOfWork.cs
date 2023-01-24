using WebApplication1.Data;

namespace WebApplication1.BL.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        Task Save();
    }
}
