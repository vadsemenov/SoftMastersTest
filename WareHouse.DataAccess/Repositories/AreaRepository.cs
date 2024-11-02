using Microsoft.EntityFrameworkCore;
using Warehouse.Core.DTO;
using Warehouse.DataAccess.Repositories.Interfaces;

namespace Warehouse.DataAccess.Repositories;

public class AreaRepository : BaseEfRepository<Area>, IAreaRepository
{
    public AreaRepository(DbContext dbContext) : base(dbContext)
    {
    }
}