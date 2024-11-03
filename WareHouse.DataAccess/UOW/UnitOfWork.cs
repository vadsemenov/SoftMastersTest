using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Warehouse.DataAccess.Repositories;
using Warehouse.DataAccess.Repositories.Interfaces;

namespace Warehouse.DataAccess.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    private IDbContextTransaction? _transaction;

    private IAreaRepository _areaRepository = null!;
    private ICargoRepository _cargoRepository = null!;
    private IPicketRepository _picketRepository = null!;
    private IWarehouseRepository _warehouseRepository = null!;

    private bool _disposed;

    public UnitOfWork(WarehouseDbContext dbContext)
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
            _areaRepository ??= new AreaRepository(_dbContext);

            return (_areaRepository as T)!;
        }

        if (typeof(T) == typeof(ICargoRepository))
        {
            _cargoRepository ??= new CargoRepository(_dbContext);

            return (_cargoRepository as T)!;
        }

        if (typeof(T) == typeof(IPicketRepository))
        {
            _picketRepository ??= new PicketRepository(_dbContext);

            return (_picketRepository as T)!;
        }

        if (typeof(T) == typeof(IWarehouseRepository))
        {
            _warehouseRepository ??= new WarehouseRepository(_dbContext);

            return (_warehouseRepository as T)!;
        }

        throw new Exception($"Repository {typeof(T)} does not exist!");
    }

    public async Task BeginTransactionAsync()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (_transaction != null)
        {
            throw new Exception("There is already an existing transaction!");
        }

        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (_transaction == null)
        {
            return;
        }

        await _transaction.CommitAsync();

        _transaction = null;
    }

    public async Task RollbackTransactionAsync()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        if (_transaction == null)
        {
            return;
        }

        await _transaction.RollbackAsync();

        _transaction = null;
    }

    public async Task SaveAsync()
    {
        if (_disposed)
        {
            throw new MemberAccessException("UnitOfWork is already disposed!");
        }

        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _disposed = true;

        _dbContext.Dispose();
    }
}