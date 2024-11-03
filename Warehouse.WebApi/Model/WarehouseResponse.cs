namespace Warehouse.WebApi.Model;

public class WarehouseResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<PicketResponse> Pickets { get; set; } = null!;

    public ICollection<AreaResponse> Areas { get; set; } = null!;
}