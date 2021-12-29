using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersAPI.Migrations
{
    public partial class Addscustomidentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9998,
                column: "ConcurrencyStamp",
                value: "8c0725a0-e338-4070-8de5-f441394a41ef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9999,
                column: "ConcurrencyStamp",
                value: "20a90342-9e8b-4b99-ba40-c32dad0ce68b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e720c0b7-8136-427b-b082-a0556275f8fd", "AQAAAAEAACcQAAAAECFG1xlsulKQ1ovL1OH2vSCf0quRvxk6VXwF7KxL7kPQ06fOOvJH/izewfC8sVAg8A==", "45944028-b531-49a3-92a0-ca77bd5459b1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9998,
                column: "ConcurrencyStamp",
                value: "d944b452-9843-42dc-98d7-fc16e597125e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9999,
                column: "ConcurrencyStamp",
                value: "480117ab-195b-4db2-9409-03fab70a712f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32c9e736-5bd5-4e46-86f7-fe0eacf43fc1", "AQAAAAEAACcQAAAAEK86n4OgXMkl55ok2fc7nwlCVfLaJ7yOCGB856lR4Kb6gljvb995bh/bf2ranC+a1g==", "f636c43d-0908-45e9-933b-2f20a223f0e5" });
        }
    }
}
