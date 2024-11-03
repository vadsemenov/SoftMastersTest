using Warehouse.DataAccess;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;
using Warehouse.DataAccess.Utils;

namespace Warehouse.ConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        using var context = new WarehouseDbContext();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var unitOfWork = new UnitOfWork(context);

        unitOfWork.CreateAndFillWarehouseDatabaseWithUowAsync().GetAwaiter().GetResult();

        var areas = unitOfWork.GetRepository<IAreaRepository>().GetAllAsync().GetAwaiter().GetResult();

        Console.Read();
    }
}