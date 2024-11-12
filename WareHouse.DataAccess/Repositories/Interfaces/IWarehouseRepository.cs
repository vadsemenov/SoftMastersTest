namespace Warehouse.DataAccess.Repositories.Interfaces;

public interface IWarehouseRepository : IMainRepository<Core.DTO.Warehouse>
{
    Task<ICollection<Core.DTO.Warehouse>> GetAllByTimeAsync(DateTime dateTime);
}