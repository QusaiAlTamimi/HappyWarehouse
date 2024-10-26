using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyWarehouseData.Migrations
{
    public partial class fx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ss",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ss",
                table: "Items");
        }
    }
}
