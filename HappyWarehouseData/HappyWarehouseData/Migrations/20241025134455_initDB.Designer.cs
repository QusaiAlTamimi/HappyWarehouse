﻿// <auto-generated />
using HappyWarehouseData.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyWarehouseAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241025134455_initDB")]
    partial class initDB
    {
        protected void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.35");

            modelBuilder.Entity("HappyWarehouseAPI.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CostPrice")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("MSRPPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Qty")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SKUCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("HappyWarehouseAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HappyWarehouseAPI.Models.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<int>("Country")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("HappyWarehouseAPI.Models.Item", b =>
                {
                    b.HasOne("HappyWarehouseAPI.Models.Warehouse", "Warehouse")
                        .WithMany("items")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("HappyWarehouseAPI.Models.Warehouse", b =>
                {
                    b.Navigation("items");
                });
#pragma warning restore 612, 618
        }
    }
}
