using Microsoft.EntityFrameworkCore;
using Warehouse.Core.DTO;
using Warehouse.DataAccess.Repositories.Interfaces;

namespace Warehouse.DataAccess.Repositories;

public class CargoRepository : BaseEfRepository<Cargo>, ICargoRepository
{
    public CargoRepository(DbContext dbContext) : base(dbContext)
    {
    }
}