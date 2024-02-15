using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Api.Migrations
{
    /// <inheritdoc />
    public partial class correvenements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Villes_VilleID",
                table: "Evenements");

            migrationBuilder.RenameColumn(
                name: "VilleID",
                table: "Evenements",
                newName: "VilleId");

            migrationBuilder.RenameIndex(
                name: "IX_Evenements_VilleID",
                table: "Evenements",
                newName: "IX_Evenements_VilleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Villes_VilleId",
                table: "Evenements",
                column: "VilleId",
                principalTable: "Villes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Villes_VilleId",
                table: "Evenements");

            migrationBuilder.RenameColumn(
                name: "VilleId",
                table: "Evenements",
                newName: "VilleID");

            migrationBuilder.RenameIndex(
                name: "IX_Evenements_VilleId",
                table: "Evenements",
                newName: "IX_Evenements_VilleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Villes_VilleID",
                table: "Evenements",
                column: "VilleID",
                principalTable: "Villes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
