using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Events.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evenements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateDebut = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateDeFin = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Titre = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Adresse = table.Column<string>(type: "text", nullable: false),
                    NomOrganisateur = table.Column<string>(type: "text", nullable: false),
                    Prix = table.Column<int>(type: "integer", nullable: true),
                    VilleID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evenements_Villes_VilleID",
                        column: x => x.VilleID,
                        principalTable: "Villes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nom = table.Column<int>(type: "integer", nullable: false),
                    EvenementId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Evenements_EvenementId",
                        column: x => x.EvenementId,
                        principalTable: "Evenements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombrePlaces = table.Column<int>(type: "integer", nullable: false),
                    Courriel = table.Column<string>(type: "text", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    EstValide = table.Column<bool>(type: "boolean", nullable: false),
                    EvenementID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participations_Evenements_EvenementID",
                        column: x => x.EvenementID,
                        principalTable: "Evenements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EvenementId",
                table: "Categories",
                column: "EvenementId");

            migrationBuilder.CreateIndex(
                name: "IX_Evenements_VilleID",
                table: "Evenements",
                column: "VilleID");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_EvenementID",
                table: "Participations",
                column: "EvenementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropTable(
                name: "Evenements");

            migrationBuilder.DropTable(
                name: "Villes");
        }
    }
}
