namespace Warehouse.Core.DTO;

public class Warehouse : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Picket> Pickets { get; set; } = null!;

    public virtual ICollection<Area> Areas { get; set; } = null!;
}