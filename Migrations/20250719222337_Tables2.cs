using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace storeapp3.Migrations
{
    /// <inheritdoc />
    public partial class Tables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c09c95b-39e6-4f31-90cc-dbd538eda04e", "AQAAAAIAAYagAAAAEMbP23EdN7v2irCDMTa2VVK/WqULCCfVclnF3KmxgXICNuW8Iwqzptd0I6f+o9vcIg==", "41b23ce1-484e-4708-abc0-0fe74e71af24" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "127f9136-8fc3-4d9b-895f-f177c22cb0db", "AQAAAAIAAYagAAAAEAMFJo+nfBjNd4tGUKHIzG/vzIP46Z4u2MMXJCkyUvPE3tu57hPFYCAhRZVU4PevAg==", "c973f29d-dfe3-45f6-a9b9-c67380984ca1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37c46284-becc-4f24-8563-2820d06a4f59", "AQAAAAIAAYagAAAAEH9Grk9evpEBHa0xj9/uO+OUThWHwFrPxYMNy4QZ387dCtFuwK/i1kw91Z2M30Fosw==", "c25ededc-2058-4137-8fb7-26f6ec4cc1f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "454769c8-2b2c-474c-a94d-2aaba820bd21", "AQAAAAIAAYagAAAAED3MbchrydDte9GzMIe/JllktHBmZBs6aME3PZXx0C0d7wLQUyDnvRXop2l8B5GsGQ==", "aa4d4aad-a954-4aa7-b32d-832235449c3a" });
        }
    }
}
