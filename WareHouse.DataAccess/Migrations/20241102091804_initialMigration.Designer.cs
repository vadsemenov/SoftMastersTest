﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Warehouse.DataAccess;

#nullable disable

namespace Warehouse.DataAccess.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    [Migration("20241102091804_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("PicketsAreas", b =>
                {
                    b.Property<int>("AreasId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PicketsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AreasId", "PicketsId");

                    b.HasIndex("PicketsId");

                    b.ToTable("PicketsAreas");
                });

            modelBuilder.Entity("Warehouse.Core.DTO.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CargoId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Warehouse.Core.DTO.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LoadTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UnloadTime")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Weight")
                        .HasPrecision(3)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cargoes");
                });

            modelBuilder.Entity("Warehouse.Core.DTO.Picket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Pickets");
                });

            modelBuilder.Entity("Warehouse.Core.DTO.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("PicketsAreas", b =>
                {
                    b.HasOne("Warehouse.Core.DTO.Area", null)
                        .WithMany()
                        .HasForeignKey("AreasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Warehouse.Core.DTO.Picket", null)
                        .WithMany()
                        .HasForeignKey("PicketsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Warehouse.Core.DTO.Area", b =>
                {
                    b.HasOne("Warehouse.Core.DTO.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Warehouse.Core.DTO.Warehouse", "Warehouse")
                        .WithMany("Areas")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Warehouse.Core.DTO.Picket", b =>
                {
                    b.HasOne("Warehouse.Core.DTO.Warehouse", "Warehouse")
                        .WithMany("Pickets")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Warehouse.Core.DTO.Warehouse", b =>
                {
                    b.Navigation("Areas");

                    b.Navigation("Pickets");
                });
#pragma warning restore 612, 618
        }
    }
}