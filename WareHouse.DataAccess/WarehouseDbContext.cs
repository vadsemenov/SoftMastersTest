using Microsoft.EntityFrameworkCore;
using Warehouse.Core.DTO;

namespace Warehouse.DataAccess;

public class WarehouseDbContext : DbContext
{
    public virtual DbSet<Area> Areas { get; set; } = null!;

    public virtual DbSet<Cargo> Cargoes { get; set; } = null!;

    public virtual DbSet<Picket> Pickets { get; set; } = null!;

    public virtual DbSet<Core.DTO.Warehouse> Warehouses { get; set; } = null!;

    public WarehouseDbContext()
    {
    }

    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options):base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=warehouse.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.DTO.Warehouse>(builder =>
        {
            builder.Property(warehouse => warehouse.Name)
                .HasMaxLength(60);
        });

        modelBuilder.Entity<Area>()
            .HasMany(e => e.Pickets)
            .WithMany(e => e.Areas)
            .UsingEntity("PicketsAreas");

        modelBuilder.Entity<Area>(builder =>
        {
            builder.Property(area => area.Name)
                .HasMaxLength(60);
        });

        modelBuilder.Entity<Cargo>(builder =>
        {
            builder.Property(cargo => cargo.Weight)
                .HasPrecision(3);
        });

        // modelBuilder.Entity<Picket>(builder =>
        // {
        //     builder.Property(picket => picket.Name)
        //         .HasMaxLength(60);
        // });
    }
}

