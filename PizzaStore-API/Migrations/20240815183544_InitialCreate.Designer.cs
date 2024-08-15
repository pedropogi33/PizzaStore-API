﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaStore_API.Models;

#nullable disable

namespace PizzaStore_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240815183544_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PizzaStore_API.Models.Order", b =>
                {
                    b.Property<int>("order_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("time")
                        .HasColumnType("time");

                    b.HasKey("order_id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PizzaStore_API.Models.OrderDetail", b =>
                {
                    b.Property<int>("order_details_id")
                        .HasColumnType("int");

                    b.Property<int>("order_id")
                        .HasColumnType("int");

                    b.Property<string>("pizza_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("order_details_id");

                    b.HasIndex("order_id");

                    b.HasIndex("pizza_id");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("PizzaStore_API.Models.Pizza", b =>
                {
                    b.Property<string>("pizza_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("pizza_type_id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pizzatypepizza_type_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("pizza_id");

                    b.HasIndex("pizzatypepizza_type_id");

                    b.ToTable("Pizzas");
                });

            modelBuilder.Entity("PizzaStore_API.Models.PizzaType", b =>
                {
                    b.Property<string>("pizza_type_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ingredients")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("pizza_type_id");

                    b.ToTable("PizzaTypes");
                });

            modelBuilder.Entity("PizzaStore_API.Models.OrderDetail", b =>
                {
                    b.HasOne("PizzaStore_API.Models.Order", "order")
                        .WithMany("orderdetails")
                        .HasForeignKey("order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PizzaStore_API.Models.Pizza", "pizza")
                        .WithMany()
                        .HasForeignKey("pizza_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");

                    b.Navigation("pizza");
                });

            modelBuilder.Entity("PizzaStore_API.Models.Pizza", b =>
                {
                    b.HasOne("PizzaStore_API.Models.PizzaType", "pizzatype")
                        .WithMany("pizzas")
                        .HasForeignKey("pizzatypepizza_type_id");

                    b.Navigation("pizzatype");
                });

            modelBuilder.Entity("PizzaStore_API.Models.Order", b =>
                {
                    b.Navigation("orderdetails");
                });

            modelBuilder.Entity("PizzaStore_API.Models.PizzaType", b =>
                {
                    b.Navigation("pizzas");
                });
#pragma warning restore 612, 618
        }
    }
}
