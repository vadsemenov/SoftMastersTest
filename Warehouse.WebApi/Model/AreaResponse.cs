﻿using Warehouse.Core.DTO;

namespace Warehouse.WebApi.Model;

public class AreaResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateTime { get; set; } 

    public DateTime DeleteTime { get; set; } 

    public CargoResponse Cargo { get; set; } = null!;

    public ICollection<PicketResponse> Pickets { get; set; } = null!;
}