namespace Warehouse.Core.DTO;

public class Area : BaseEntity
{
    public string Name { get; set; } = null!;

    public DateTime CreateTime { get; set; }

    public DateTime? DeleteTime { get; set; }
    
    public virtual Cargo Cargo { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;

    public virtual ICollection<Picket> Pickets { get; set; } = null!;
}