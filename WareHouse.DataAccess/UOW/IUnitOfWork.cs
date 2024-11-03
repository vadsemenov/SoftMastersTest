using Warehouse.DataAccess.Repositories.Interfaces;

namespace Warehouse.DataAccess.UOW;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();

    Task SaveAsync();

    T GetRepository<T>() where T : class, IRepository;
}