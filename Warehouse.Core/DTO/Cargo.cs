namespace Warehouse.Core.DTO;

public class Cargo : BaseEntity
{
    public decimal Weight { get; set; }

    public DateTime LoadTime { get; set; }

    public DateTime? UnloadTime { get; set; }
}