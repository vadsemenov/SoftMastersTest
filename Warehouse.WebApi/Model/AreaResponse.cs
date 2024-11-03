using Warehouse.Core.DTO;

namespace Warehouse.WebApi.Model;

public class AreaResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string CreateTime { get; set; } = string.Empty;

    public string DeleteTime { get; set; } = string.Empty;

    public CargoResponse Cargo { get; set; } = null!;

    public ICollection<PicketResponse> Pickets { get; set; } = null!;
}