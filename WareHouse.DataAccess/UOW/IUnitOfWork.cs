using Warehouse.DataAccess.Repositories.Interfaces;

namespace Warehouse.DataAccess.UOW;

public interface IUnitOfWork : IDisposable
{
    void Save();

    T GetRepository<T>() where T : class, IRepository;
}