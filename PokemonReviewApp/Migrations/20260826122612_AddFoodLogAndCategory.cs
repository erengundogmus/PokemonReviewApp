using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodLogAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodLogs");

            migrationBuilder.CreateTable(
                name: "CategoryLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldHp = table.Column<int>(type: "int", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewHp = table.Column<int>(type: "int", nullable: true),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryLog");

            migrationBuilder.DropTable(
                name: "FoodLog");

            migrationBuilder.CreateTable(
                name: "FoodLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewHp = table.Column<int>(type: "int", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldHp = table.Column<int>(type: "int", nullable: true),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodLogs", x => x.Id);
                });
        }
    }
}
