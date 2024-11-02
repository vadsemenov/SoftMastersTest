using Microsoft.VisualBasic;
using Warehouse.Core.DTO;
using Warehouse.DataAccess;
using Warehouse.DataAccess.Repositories.Interfaces;
using Warehouse.DataAccess.UOW;
using Warehouse.Utils;
using StringComparer = Warehouse.Utils.StringComparer;

namespace Warehouse.ConsoleApp;

public static class DatabaseService
{
    public static void CreateAndFillWarehouseDatabaseWithUow(this UnitOfWork unitOfWork)
    {
        var warehouseRepository = unitOfWork.GetRepository<IWarehouseRepository>();
        var picketRepository = unitOfWork.GetRepository<IPicketRepository>();
        var cargoRepository = unitOfWork.GetRepository<ICargoRepository>();
        var areaRepository = unitOfWork.GetRepository<IAreaRepository>();

        try
        {
            unitOfWork.BeginTransaction();

            var warehouse = new Core.DTO.Warehouse
            {
                Name = "Warehouse1"
            };

            warehouseRepository.Create(warehouse);

            var picket1 = new Picket
            {
                Name = "101",
                Warehouse = warehouse
            };

            var picket2 = new Picket
            {
                Name = "102",
                Warehouse = warehouse
            };

            var picket3 = new Picket
            {
                Name = "103",
                Warehouse = warehouse
            };

            var picket4 = new Picket
            {
                Name = "104",
                Warehouse = warehouse
            };

            picketRepository.Create(new List<Picket>{picket1, picket2, picket3, picket4});

            var cargo1 = new Cargo
            {
                LoadTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("10:00:00")),
                Weight = 5002
            };

            var cargo2 = new Cargo
            {
                LoadTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("15:00:00")),
                Weight = 3000
            };

            var cargo3 = new Cargo
            {
                LoadTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("15:00:00")),
                Weight = 4000
            };

            cargoRepository.Create(new List<Cargo>{cargo1, cargo2});

            var area1Pickets = new List<Picket>
            {
                picket1, picket2
            }
                .OrderBy(p => p.Name, new StringComparer())
                .ToList();

            var area2Pickets = new List<Picket>
            {
                picket3
            }
                .OrderBy(p => p.Name, new StringComparer())
                .ToList();

            var area3Pickets = new List<Picket>
            {
                picket4
            }
                .OrderBy(p => p.Name, new StringComparer())
                .ToList();

            var area1 = new Area
            {
                Name = area1Pickets.GetAreaName(),
                Warehouse = warehouse,
                Cargo = cargo1,
                Pickets = area1Pickets,
                CreateTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("10:00:00"))
            };

            var area2 = new Area
            {
                Name = area2Pickets.GetAreaName(),
                Warehouse = warehouse,
                Cargo = cargo2,
                Pickets = area2Pickets,
                CreateTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("15:00:00"))
            };

            var area3 = new Area
            {
                Name = area3Pickets.GetAreaName(),
                Warehouse = warehouse,
                Cargo = cargo3,
                Pickets = area3Pickets,
                CreateTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("15:00:00"))
            };

            areaRepository.Create(new List<Area>{area1, area2, area3});

            unitOfWork.CommitTransaction();

            unitOfWork.Save();
        }
        catch
        {
            unitOfWork.RollbackTransaction();
        }
    }

    public static void CreateAndFillWarehouseDatabase(this WarehouseDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var warehouse = new Core.DTO.Warehouse
        {
            Name = "Warehouse1"
        };

        context.Warehouses.Add(warehouse);
        context.SaveChanges();

        var picket1 = new Picket
        {
            Name = "101",
            Warehouse = warehouse
        };

        var picket2 = new Picket
        {
            Name = "102",
            Warehouse = warehouse
        };

        var picket3 = new Picket
        {
            Name = "103",
            Warehouse = warehouse
        };

        var picket4 = new Picket
        {
            Name = "104",
            Warehouse = warehouse
        };

        context.Pickets.AddRange(picket1, picket2, picket3, picket4);
        context.SaveChanges();

        var cargo1 = new Cargo
        {
            LoadTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("10:00:00")),
            Weight = 5002
        };

        var cargo2 = new Cargo
        {
            LoadTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("15:00:00")),
            Weight = 3000
        };

        var cargo3 = new Cargo
        {
            LoadTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("15:00:00")),
            Weight = 4000
        };

        context.Cargoes.AddRange(cargo1, cargo2);
        context.SaveChanges();

        var area1Pickets = new List<Picket>
            {
                picket1, picket2
            }
            .OrderBy(p => p.Name, new StringComparer())
            .ToList();

        var area2Pickets = new List<Picket>
            {
                picket3
            }
            .OrderBy(p => p.Name, new StringComparer())
            .ToList();

        var area3Pickets = new List<Picket>
            {
                picket4
            }
            .OrderBy(p => p.Name, new StringComparer())
            .ToList();

        var area1 = new Area
        {
            Name = area1Pickets.GetAreaName(),
            Warehouse = warehouse,
            Cargo = cargo1,
            Pickets = area1Pickets,
            CreateTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("10:00:00"))
        };

        var area2 = new Area
        {
            Name = area2Pickets.GetAreaName(),
            Warehouse = warehouse,
            Cargo = cargo2,
            Pickets = area2Pickets,
            CreateTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("15:00:00"))
        };

        var area3 = new Area
        {
            Name = area3Pickets.GetAreaName(),
            Warehouse = warehouse,
            Cargo = cargo3,
            Pickets = area3Pickets,
            CreateTime = new DateTime(new DateOnly(2024, 10, 31), TimeOnly.Parse("15:00:00"))
        };

        context.Areas.AddRange(area1, area2, area3);
        context.SaveChanges();
    }
}