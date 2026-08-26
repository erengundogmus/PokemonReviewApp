using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPokemonLogAndCountryAndOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonCategories_Pokemon_PokemonId",
                table: "PokemonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonFoods_Pokemon_PokemonId",
                table: "PokemonFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonsOwners_Pokemon_PokemonId",
                table: "PokemonsOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Pokemon_PokemonId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pokemon",
                table: "Pokemon");

            migrationBuilder.RenameTable(
                name: "Pokemon",
                newName: "Pokemons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pokemons",
                table: "Pokemons",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CountryLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldGym = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewGym = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PokemonId = table.Column<int>(type: "int", nullable: false),
                    OldName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldBirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OldOwnerId = table.Column<int>(type: "int", nullable: true),
                    OldCategoryId = table.Column<int>(type: "int", nullable: true),
                    NewName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewBirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NewOwnerId = table.Column<int>(type: "int", nullable: true),
                    NewCategoryId = table.Column<int>(type: "int", nullable: true),
                    LoggedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonLog", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonCategories_Pokemons_PokemonId",
                table: "PokemonCategories",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonFoods_Pokemons_PokemonId",
                table: "PokemonFoods",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonsOwners_Pokemons_PokemonId",
                table: "PokemonsOwners",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Pokemons_PokemonId",
                table: "Reviews",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonCategories_Pokemons_PokemonId",
                table: "PokemonCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonFoods_Pokemons_PokemonId",
                table: "PokemonFoods");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonsOwners_Pokemons_PokemonId",
                table: "PokemonsOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Pokemons_PokemonId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "CountryLog");

            migrationBuilder.DropTable(
                name: "OwnerLog");

            migrationBuilder.DropTable(
                name: "PokemonLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pokemons",
                table: "Pokemons");

            migrationBuilder.RenameTable(
                name: "Pokemons",
                newName: "Pokemon");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pokemon",
                table: "Pokemon",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonCategories_Pokemon_PokemonId",
                table: "PokemonCategories",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonFoods_Pokemon_PokemonId",
                table: "PokemonFoods",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonsOwners_Pokemon_PokemonId",
                table: "PokemonsOwners",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Pokemon_PokemonId",
                table: "Reviews",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
