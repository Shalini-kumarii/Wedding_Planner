using Microsoft.EntityFrameworkCore.Migrations;

namespace Wedding_Planner.Migrations
{
    public partial class Wedding4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Weddings",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Weddings");
        }
    }
}
