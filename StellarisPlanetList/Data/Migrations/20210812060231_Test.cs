using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StellarisPlanetList.Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    PlanetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanetName = table.Column<string>(maxLength: 180, nullable: false),
                    PlanetType = table.Column<string>(maxLength: 180, nullable: false),
                    PlanetSize = table.Column<int>(maxLength: 3, nullable: false),
                    PlanetPercenage = table.Column<int>(maxLength: 3, nullable: false),
                    PlanetEffects = table.Column<string>(maxLength: 300, nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    PlanetNotes = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.PlanetId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
