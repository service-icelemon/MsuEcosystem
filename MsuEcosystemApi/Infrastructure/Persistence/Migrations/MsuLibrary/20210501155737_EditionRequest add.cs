using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.MsuLibrary
{
    public partial class EditionRequestadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedEditions_Editions_EditionId",
                table: "BorrowedEditions");

            migrationBuilder.DropColumn(
                name: "ReaderId",
                table: "BorrowedEditions");

            migrationBuilder.RenameColumn(
                name: "EditionId",
                table: "BorrowedEditions",
                newName: "RequestId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedEditions_EditionId",
                table: "BorrowedEditions",
                newName: "IX_BorrowedEditions_RequestId");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PickUpPoints",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickUpPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EditionRequests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EditionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReaderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    IsPickedUp = table.Column<bool>(type: "bit", nullable: false),
                    PickUpPointId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditionRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EditionRequests_PickUpPoints_PickUpPointId",
                        column: x => x.PickUpPointId,
                        principalTable: "PickUpPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditionRequests_PickUpPointId",
                table: "EditionRequests",
                column: "PickUpPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedEditions_EditionRequests_RequestId",
                table: "BorrowedEditions",
                column: "RequestId",
                principalTable: "EditionRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedEditions_EditionRequests_RequestId",
                table: "BorrowedEditions");

            migrationBuilder.DropTable(
                name: "EditionRequests");

            migrationBuilder.DropTable(
                name: "PickUpPoints");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "BorrowedEditions",
                newName: "EditionId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowedEditions_RequestId",
                table: "BorrowedEditions",
                newName: "IX_BorrowedEditions_EditionId");

            migrationBuilder.AddColumn<string>(
                name: "ReaderId",
                table: "BorrowedEditions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedEditions_Editions_EditionId",
                table: "BorrowedEditions",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
