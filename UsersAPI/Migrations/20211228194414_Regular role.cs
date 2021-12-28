using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersAPI.Migrations
{
    public partial class Regularrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9999,
                column: "ConcurrencyStamp",
                value: "480117ab-195b-4db2-9409-03fab70a712f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 9998, "d944b452-9843-42dc-98d7-fc16e597125e", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32c9e736-5bd5-4e46-86f7-fe0eacf43fc1", "AQAAAAEAACcQAAAAEK86n4OgXMkl55ok2fc7nwlCVfLaJ7yOCGB856lR4Kb6gljvb995bh/bf2ranC+a1g==", "f636c43d-0908-45e9-933b-2f20a223f0e5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9998);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 9999,
                column: "ConcurrencyStamp",
                value: "4fc4ad7f-dd23-4931-b6b5-ee3c12c623b8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1cd90433-f941-4a3e-bdce-b13ccd59fe9a", "AQAAAAEAACcQAAAAELFSh2YyismNC+NPPMiZnGSORNx83Gdp0Z13J5+XDDtYGCfiz5Co8GatZ0QdkChu4Q==", "3407d28e-7cea-482a-bd04-c557843ad248" });
        }
    }
}
