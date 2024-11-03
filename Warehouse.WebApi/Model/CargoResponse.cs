namespace Warehouse.WebApi.Model;

public class CargoResponse
{
    public int Id { get; set; }

    public string Weight { get; set; } = null!;

    public string LoadTime { get; set; } = null!;

    public string UnloadTime { get; set; } = null!;
}