using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HST.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "157e1372-f5e6-4480-8ecb-787237484227", "AQAAAAIAAYagAAAAEEcTgq/C7R71UWeSL7d2byXQvSHRbxMBR8KyJpGPf2HL280aG3M54O27UcTgh53Snw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "84b590b3-068d-480f-8ba7-83e6dcef8bf0", "AQAAAAIAAYagAAAAEBU0Y1ga09lFR2bbc4ZEhSwAl940Y8evY/HeejdUHWSVD+FQlM5MamBY5OGEnoFPug==" });

            migrationBuilder.UpdateData(
                table: "CustomerAccountProcesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProcessDate",
                value: new DateTime(2024, 9, 5, 16, 55, 25, 386, DateTimeKind.Local).AddTicks(5650));

            migrationBuilder.UpdateData(
                table: "CustomerAccountProcesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProcessDate",
                value: new DateTime(2024, 9, 4, 16, 55, 25, 386, DateTimeKind.Local).AddTicks(5650));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "add09b84-81e9-448a-b1be-cfb8cf65b443", "AQAAAAIAAYagAAAAEDkusB+4Z9QYqfDVnK4jY6cGBPfc9aBc+QbNJjb8A4tywWYshIzjUj6a8JNJiZEfaw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e0e70a76-db90-4679-a353-9f1bc2396eaa", "AQAAAAIAAYagAAAAEFqBveto9y+1ertf0xgzfn7aqNYOcHHPLgpzM7Qh4VrApRFD5QeeTyk3mT+gfkv3aw==" });

            migrationBuilder.UpdateData(
                table: "CustomerAccountProcesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProcessDate",
                value: new DateTime(2024, 9, 5, 15, 58, 17, 409, DateTimeKind.Local).AddTicks(4730));

            migrationBuilder.UpdateData(
                table: "CustomerAccountProcesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProcessDate",
                value: new DateTime(2024, 9, 4, 15, 58, 17, 409, DateTimeKind.Local).AddTicks(4740));
        }
    }
}
