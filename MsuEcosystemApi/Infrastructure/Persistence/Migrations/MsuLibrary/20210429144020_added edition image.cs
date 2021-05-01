using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.MsuLibrary
{
    public partial class addededitionimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Editions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Editions");
        }
    }
}
