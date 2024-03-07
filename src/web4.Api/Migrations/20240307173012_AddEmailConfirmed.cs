using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bd2f030-453b-45a1-89a2-9cade395d7c1",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a996185-de4e-45e5-8fc6-4d44b1f5d847", true, "AQAAAAIAAYagAAAAEKOQHPpdBSxRTlcvwRGXMDrhYmVfy9TVGky6/PwfnHMyohV50SiUohr2nLQmd1l+BA==", "fb76e176-0ec4-41f6-bea7-01b6d38e5257" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af825a36-d24e-469b-8b04-1f1729a0e908", true, "AQAAAAIAAYagAAAAEJoUbVY9rFy5j4cZtKUizF6ULECJYKRQWv0Hw/o7vgsNdIMg68XqtxhZ1t8jqPnBRA==", "7b740072-efe1-4283-86af-e9a14c91ccd0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3bd2f030-453b-45a1-89a2-9cade395d7c1",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8eee0aec-48be-439a-a267-084dca4fd6e7", false, "AQAAAAIAAYagAAAAEGMHjleEbo1PJhikhDvYTOxCbm7HWgvK9OLSkcELJNpBkU3p77bWYBVgkG7JfWzesw==", "2cad9cf3-c6a2-45a6-ad28-47964e25e93c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ce30e12-cd20-49cb-b83d-8b9879c2eb30", false, "AQAAAAIAAYagAAAAEGC+DlsaJbUD8TnsDhc2Vh4+j8HLJDvd7s8eDsfQLtR0KEBt0fp0mkmvdvvRyQNW5w==", "244fc750-7621-434d-8cd0-cf92240366d8" });
        }
    }
}
