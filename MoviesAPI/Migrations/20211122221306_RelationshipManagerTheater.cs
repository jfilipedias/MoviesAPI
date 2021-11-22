using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoviesAPI.Migrations
{
    public partial class RelationshipManagerTheater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Theaters_ManagerId",
                table: "Theaters",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theaters_Manager_ManagerId",
                table: "Theaters",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theaters_Manager_ManagerId",
                table: "Theaters");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropIndex(
                name: "IX_Theaters_ManagerId",
                table: "Theaters");
        }
    }
}
