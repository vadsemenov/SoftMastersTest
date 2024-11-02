using Microsoft.EntityFrameworkCore;
using Warehouse.Core.DTO;
using Warehouse.DataAccess.Repositories.Interfaces;

namespace Warehouse.DataAccess.Repositories;

public class PicketRepository : BaseEfRepository<Picket>, IPicketRepository
{
    public PicketRepository(DbContext dbContext) : base(dbContext)
    {
    }
}