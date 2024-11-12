using Microsoft.EntityFrameworkCore;
using Warehouse.Core.DTO;
using Warehouse.DataAccess.Repositories.Interfaces;

namespace Warehouse.DataAccess.Repositories;

public class WarehouseRepository : BaseEfRepository<Core.DTO.Warehouse>, IWarehouseRepository
{
    public WarehouseRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<Core.DTO.Warehouse>> GetAllByTimeAsync(DateTime dateTime)
    {

        // var warehouses  =  await DbSet.ToListAsync();
        //
        // return warehouses.Select(w =>
        // {
        //     w.Areas = w.Areas.Where(a=> a.CreateTime <= dateTime && a.DeleteTime == null)
        //         .Where().ToList();
        //     w.Areas = w.Areas.Select(a => a.)
        //
        //     return w;
        // }).ToList()


        return await DbSet.ToListAsync();
    }
}