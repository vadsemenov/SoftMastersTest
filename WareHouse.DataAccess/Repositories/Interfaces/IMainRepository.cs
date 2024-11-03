namespace Warehouse.DataAccess.Repositories.Interfaces;

public interface IMainRepository<T> : IRepository where T : class
{
    void Create(T entity);

    void Create(ICollection<T> entities);

    void Update(T entity);

    void Delete(T entity);

    Task<ICollection<T>> GetAllAsync();

    Task<T?> GetByIdAsync(int id);

    Task SaveAsync();
}