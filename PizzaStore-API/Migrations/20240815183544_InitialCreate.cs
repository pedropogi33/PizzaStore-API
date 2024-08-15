﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaStore_API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    time = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.order_id);
                });

            migrationBuilder.CreateTable(
                name: "PizzaTypes",
                columns: table => new
                {
                    pizza_type_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaTypes", x => x.pizza_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Pizzas",
                columns: table => new
                {
                    pizza_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    pizza_type_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    pizzatypepizza_type_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizzas", x => x.pizza_id);
                    table.ForeignKey(
                        name: "FK_Pizzas_PizzaTypes_pizzatypepizza_type_id",
                        column: x => x.pizzatypepizza_type_id,
                        principalTable: "PizzaTypes",
                        principalColumn: "pizza_type_id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    order_details_id = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    pizza_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.order_details_id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Pizzas_pizza_id",
                        column: x => x.pizza_id,
                        principalTable: "Pizzas",
                        principalColumn: "pizza_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_order_id",
                table: "OrderDetails",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_pizza_id",
                table: "OrderDetails",
                column: "pizza_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_pizzatypepizza_type_id",
                table: "Pizzas",
                column: "pizzatypepizza_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pizzas");

            migrationBuilder.DropTable(
                name: "PizzaTypes");
        }
    }
}
