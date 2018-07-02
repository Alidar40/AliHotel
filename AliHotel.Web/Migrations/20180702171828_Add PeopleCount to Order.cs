using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AliHotel.Web.Migrations
{
    public partial class AddPeopleCounttoOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerMen",
                table: "RoomTypes",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PeopleCount",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeopleCount",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "PricePerMen",
                table: "RoomTypes",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
