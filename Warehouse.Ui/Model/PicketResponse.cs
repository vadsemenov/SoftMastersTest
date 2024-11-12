namespace Warehouse.Ui.Model;

public class PicketResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int WarehouseId { get; set; }

    // public ICollection<AreaResponse> Areas { get; set; } = null!;
}