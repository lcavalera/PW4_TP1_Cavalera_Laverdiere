using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Api.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Evenements_EvenementID",
                table: "Participations");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Evenements_EvenementID",
                table: "Participations",
                column: "EvenementID",
                principalTable: "Evenements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Evenements_EvenementID",
                table: "Participations");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Evenements_EvenementID",
                table: "Participations",
                column: "EvenementID",
                principalTable: "Evenements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
