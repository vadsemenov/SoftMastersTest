using System.Globalization;
using Warehouse.Core.DTO;
using Warehouse.WebApi.Model;

namespace Warehouse.WebApi.Map;

public static class Mapper
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

    public static CargoResponse ToModel(this Cargo cargoDto)
    {
        return new CargoResponse
        {
            Id = cargoDto.Id,
            LoadTime = cargoDto.LoadTime.ToShortDateString(),
            UnloadTime = cargoDto.UnloadTime?.ToShortDateString() ?? string.Empty,
            Weight = cargoDto.Weight.ToString(CultureInfo.InvariantCulture)
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

    public static AreaResponse ToModel(this Area areaDto)
    {
        return new AreaResponse
        {
            Id = areaDto.Id,
            Name = areaDto.Name,
            Cargo = areaDto.Cargo.ToModel(),
            CreateTime = areaDto.CreateTime.ToShortDateString(),
            DeleteTime = areaDto.DeleteTime?.ToShortDateString() ?? string.Empty,
            Pickets = areaDto.Pickets.Select(p => p.ToModel()).ToList()
        };
    }
}