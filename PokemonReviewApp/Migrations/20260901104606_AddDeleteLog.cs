using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PerformedBy",
                table: "ReviewLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ReviewLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerformedBy",
                table: "ReviewerLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ReviewerLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerformedBy",
                table: "PokemonLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PokemonLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerformedBy",
                table: "PokemonFoodLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PokemonFoodLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerformedBy",
                table: "OwnerLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OwnerLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerformedBy",
                table: "FoodLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "FoodLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerformedBy",
                table: "CountryLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CountryLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerformedBy",
                table: "CategoryLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CategoryLog",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "ReviewLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ReviewLog");

            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "ReviewerLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ReviewerLog");

            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "PokemonLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PokemonLog");

            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "PokemonFoodLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PokemonFoodLog");

            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "OwnerLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OwnerLog");

            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "FoodLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FoodLog");

            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "CountryLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CountryLog");

            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "CategoryLog");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CategoryLog");
        }
    }
}
