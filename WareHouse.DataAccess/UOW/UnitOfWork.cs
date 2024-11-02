using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Warehouse.DataAccess.Repositories;
using Warehouse.DataAccess.Repositories.Interfaces;

namespace Warehouse.DataAccess.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    private IDbContextTransaction? _transaction;

    private bool _disposed;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext ?? throw new Exception("DbContext is null!");
    }

    public T GetRepository<T>() where T : class, IRepository
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (typeof(T) == typeof(IAreaRepository))
        {
            return (new AreaRepository(_dbContext) as T)!;
        }

        if (typeof(T) == typeof(ICargoRepository))
        {
            return (new CargoRepository(_dbContext) as T)!;
        }

        if (typeof(T) == typeof(IPicketRepository))
        {
            return (new PicketRepository(_dbContext) as T)!;
        }

        if (typeof(T) == typeof(IWarehouseRepository))
        {
            return (new WarehouseRepository(_dbContext) as T)!;
        }

        throw new Exception($"Repository {typeof(T)} does not exist!");
    }

    public void BeginTransaction()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (_transaction != null)
        {
            throw new Exception("There is already an existing transaction!");
        }

        _transaction = _dbContext.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (_transaction == null)
        {
            return;
        }

        _transaction.Commit();

        _transaction = null;
    }

    public void RollbackTransaction()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (_transaction == null)
        {
            return;
        }

        _transaction.Rollback();

        _transaction = null;
    }

    public void Save()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _disposed = true;

        _dbContext.Dispose();
    }
}