using System.Globalization;
using Warehouse.Core.DTO;
using Warehouse.Model.Model;

namespace Warehouse.WebApi.Map;

//Сервис конвертирует классы из DTO в Rest 
public static class MapperService
{
    public static WarehouseResponse ToModel(this Core.DTO.Warehouse warehouseDto)
    {
        return new WarehouseResponse
        {
            Id = warehouseDto.Id,
            Name = warehouseDto.Name,
            Areas = warehouseDto.Areas.Select(a => a.ToModel()).ToList(),
            Pickets = warehouseDto.Pickets.Select(p => p.ToModel()).ToList()
        };
    }

    public static Core.DTO.Warehouse ToDto(this WarehouseResponse warehouse)
    {
        return new Core.DTO.Warehouse
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Areas = warehouse.Areas.Select(a => a.ToDto()).ToList(),
            Pickets = warehouse.Pickets.Select(p => p.ToDto()).ToList()
        };
    }

    public static AreaResponse ToModel(this Area areaDto)
    {
        return new AreaResponse
        {
            Id = areaDto.Id,
            Name = areaDto.Name,
            Cargo = areaDto.Cargoes.Select(c => c.ToModel()).ToList(),
            CreateTime = areaDto.CreateTime,
            DeleteTime = areaDto.DeleteTime ?? DateTime.MinValue,
            Pickets = areaDto.Pickets.Select(p => p.ToModel()).ToList()
        };
    }

    public static Area ToDto(this AreaResponse area)
    {
        return new Area
        {
            Id = area.Id,
            Name = area.Name,
            CreateTime = area.CreateTime,
            DeleteTime = area.DeleteTime,
            Cargoes = new List<Cargo>(),
            Pickets = area.Pickets.Select(p => p.ToDto()).ToList()
        };
    }

    public static PicketResponse ToModel(this Picket picketDto)
    {
        return new PicketResponse
        {
            Id = picketDto.Id,
            Name = picketDto.Name
        };
    }

    public static Picket ToDto(this PicketResponse picket)
    {
        return new Picket
        {
            Id = picket.Id,
            Name = picket.Name,
            Areas = new List<Area>()
        };
    }

    public static CargoResponse ToModel(this Cargo cargoDto)
    {
        return new CargoResponse
        {
            Id = cargoDto.Id,
            LoadTime = cargoDto.LoadTime,
            UnloadTime = cargoDto.UnloadTime ?? DateTime.MinValue,
            Weight = cargoDto.Weight.ToString("##.###", CultureInfo.InvariantCulture)
        };
    }
    public static Cargo ToDto(this CargoResponse cargoResponse)
    {
        var weight = 0.000M;
        decimal.TryParse(cargoResponse.Weight, out weight);

        return new Cargo
        {
            Id = cargoResponse.Id,
            LoadTime = cargoResponse.LoadTime,
            Weight = weight
        };
    }
}