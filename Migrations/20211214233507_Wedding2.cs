using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wedding_Planner.Migrations
{
    public partial class Wedding2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Weddings");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Weddings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "WedderOne",
                table: "Weddings",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "WedderTwo",
                table: "Weddings",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "WedderOne",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "WedderTwo",
                table: "Weddings");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Weddings",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Weddings",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");
        }
    }
}
