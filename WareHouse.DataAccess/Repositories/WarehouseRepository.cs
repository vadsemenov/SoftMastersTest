using Microsoft.EntityFrameworkCore;
using Warehouse.DataAccess.Repositories.Interfaces;

namespace Warehouse.DataAccess.Repositories;

public class WarehouseRepository : BaseEfRepository<Core.DTO.Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(DbContext dbContext) : base(dbContext)
    {
    }
}