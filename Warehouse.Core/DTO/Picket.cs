namespace Warehouse.Core.DTO;

public class Picket : BaseEntity
{
    public int Name { get; set; }

    public virtual Warehouse Warehouse { get; set; } = null!;

    public virtual ICollection<Area>? Areas { get; set; } = null!;
}