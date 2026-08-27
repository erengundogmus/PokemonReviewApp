using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldColumnsFromLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldPokemonId",
                table: "ReviewLog");

            migrationBuilder.DropColumn(
                name: "OldRating",
                table: "ReviewLog");

            migrationBuilder.DropColumn(
                name: "OldReviewerId",
                table: "ReviewLog");

            migrationBuilder.DropColumn(
                name: "OldText",
                table: "ReviewLog");

            migrationBuilder.DropColumn(
                name: "OldTitle",
                table: "ReviewLog");

            migrationBuilder.DropColumn(
                name: "OldFirstName",
                table: "ReviewerLog");

            migrationBuilder.DropColumn(
                name: "OldLastName",
                table: "ReviewerLog");

            migrationBuilder.DropColumn(
                name: "OldBirthDate",
                table: "PokemonLog");

            migrationBuilder.DropColumn(
                name: "OldCategoryId",
                table: "PokemonLog");

            migrationBuilder.DropColumn(
                name: "OldName",
                table: "PokemonLog");

            migrationBuilder.DropColumn(
                name: "OldOwnerId",
                table: "PokemonLog");

            migrationBuilder.DropColumn(
                name: "OldGym",
                table: "OwnerLog");

            migrationBuilder.DropColumn(
                name: "OldName",
                table: "OwnerLog");

            migrationBuilder.DropColumn(
                name: "OldHp",
                table: "FoodLog");

            migrationBuilder.DropColumn(
                name: "OldName",
                table: "FoodLog");

            migrationBuilder.DropColumn(
                name: "OldName",
                table: "CountryLog");

            migrationBuilder.DropColumn(
                name: "OldName",
                table: "CategoryLog");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OldPokemonId",
                table: "ReviewLog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldRating",
                table: "ReviewLog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldReviewerId",
                table: "ReviewLog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldText",
                table: "ReviewLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldTitle",
                table: "ReviewLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldFirstName",
                table: "ReviewerLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldLastName",
                table: "ReviewerLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OldBirthDate",
                table: "PokemonLog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldCategoryId",
                table: "PokemonLog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldName",
                table: "PokemonLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldOwnerId",
                table: "PokemonLog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldGym",
                table: "OwnerLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldName",
                table: "OwnerLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldHp",
                table: "FoodLog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldName",
                table: "FoodLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldName",
                table: "CountryLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldName",
                table: "CategoryLog",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
