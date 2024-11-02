using Warehouse.DataAccess;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;

namespace Warehouse.ConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        using var context = new WarehouseDbContext();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var unitOfWork = new UnitOfWork(context);

        unitOfWork.CreateAndFillWarehouseDatabaseWithUow();

        var areas = unitOfWork.GetRepository<IAreaRepository>().GetAll();

        Console.Read();
    }
}