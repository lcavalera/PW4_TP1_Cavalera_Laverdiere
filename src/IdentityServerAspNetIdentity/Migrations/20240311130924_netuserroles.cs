using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityServerAspNetIdentity.Migrations
{
    /// <inheritdoc />
    public partial class netuserroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "653bb917-ac53-464e-9e41-1be6fa6cf9e1", "3bd2f030-453b-45a1-89a2-9cade395d7c1" },
                    { "b4a17d23-2b27-4a35-950a-d97382cb90f4", "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3" }
                });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "3bd2f030-453b-45a1-89a2-9cade395d7c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b88f1d94-20b3-49d6-8ca2-b44498616675", "AQAAAAIAAYagAAAAELMd5/kPRUrQRCaL0h8cY4wKl305nVVV28k2Kql5aTFnVIiGGK4P77i11txMeZ/2mw==", "f771771e-8a26-4f87-a74b-2743c264a2db" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e5f9a16-c493-403c-a988-d6080f94e6b1", "AQAAAAIAAYagAAAAEPeYd9fU7IYkfkewm5xXSi3VLfbYwEvr0DXm3kfaXkJnsWOW50y2AyJ0x6/EVL0Aeg==", "74244de2-8c9b-4177-8416-ba1907559e65" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "653bb917-ac53-464e-9e41-1be6fa6cf9e1", "3bd2f030-453b-45a1-89a2-9cade395d7c1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b4a17d23-2b27-4a35-950a-d97382cb90f4", "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "3bd2f030-453b-45a1-89a2-9cade395d7c1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af911888-cf7c-4df9-b044-5f1448b34eea", "AQAAAAIAAYagAAAAEE2PB9ltL5+FbawsWQD8+JUqEbFGaKcgZ4BLboOxYgCO/BvPog4RiVByElXek6yTHg==", "a69b59e1-c5a1-442b-9eae-1e7653eacafe" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "99523801-951d-45e9-befc-b79939f663e8", "AQAAAAIAAYagAAAAEFnChZUA9QKPczQUsnZ6kCaeQfX029l23gtWav546jkKeF+xSw2noNcBMynRQKAHZA==", "f1961048-69ee-45ab-8ac7-e8f358cb400a" });
        }
    }
}
