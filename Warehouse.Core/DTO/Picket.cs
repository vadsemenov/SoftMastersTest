namespace Warehouse.Core.DTO;

public class Picket : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;

    public virtual ICollection<Area>? Areas { get; set; } = null!;
}