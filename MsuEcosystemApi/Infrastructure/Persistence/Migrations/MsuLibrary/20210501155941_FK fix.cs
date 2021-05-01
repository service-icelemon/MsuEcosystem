using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.MsuLibrary
{
    public partial class FKfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EditionId",
                table: "EditionRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EditionRequests_EditionId",
                table: "EditionRequests",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EditionRequests_Editions_EditionId",
                table: "EditionRequests",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditionRequests_Editions_EditionId",
                table: "EditionRequests");

            migrationBuilder.DropIndex(
                name: "IX_EditionRequests_EditionId",
                table: "EditionRequests");

            migrationBuilder.AlterColumn<string>(
                name: "EditionId",
                table: "EditionRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
