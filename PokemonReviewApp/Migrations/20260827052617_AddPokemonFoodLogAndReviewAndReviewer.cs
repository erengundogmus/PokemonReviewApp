using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPokemonFoodLogAndReviewAndReviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonFoodLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonFoodLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewerLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    OldFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewerLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    OldTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldRating = table.Column<int>(type: "int", nullable: true),
                    OldReviewerId = table.Column<int>(type: "int", nullable: true),
                    OldPokemonId = table.Column<int>(type: "int", nullable: true),
                    NewTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewRating = table.Column<int>(type: "int", nullable: true),
                    NewReviewerId = table.Column<int>(type: "int", nullable: true),
                    NewPokemonId = table.Column<int>(type: "int", nullable: true),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonFoodLog");

            migrationBuilder.DropTable(
                name: "ReviewerLog");

            migrationBuilder.DropTable(
                name: "ReviewLog");
        }
    }
}
