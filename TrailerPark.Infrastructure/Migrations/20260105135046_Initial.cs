using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrailerPark.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Alpha2 = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Alpha3 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Alpha2);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    imdbID = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: true),
                    Rated = table.Column<string>(type: "text", nullable: true),
                    Released = table.Column<DateOnly>(type: "date", nullable: true),
                    RuntimeMin = table.Column<int>(type: "integer", nullable: true),
                    Genre = table.Column<string>(type: "text", nullable: true),
                    Director = table.Column<List<string>>(type: "text[]", nullable: true),
                    Writer = table.Column<List<string>>(type: "text[]", nullable: true),
                    Actors = table.Column<List<string>>(type: "text[]", nullable: true),
                    Plot = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<string>(type: "text", nullable: true),
                    CountryAlpha2 = table.Column<string>(type: "text", nullable: true),
                    Awards = table.Column<string>(type: "text", nullable: true),
                    Poster = table.Column<string>(type: "text", nullable: true),
                    Metascore = table.Column<string>(type: "text", nullable: true),
                    imdbRating = table.Column<string>(type: "text", nullable: true),
                    imdbVotes = table.Column<int>(type: "integer", nullable: true),
                    TypeID = table.Column<int>(type: "integer", nullable: true),
                    DVD = table.Column<string>(type: "text", nullable: true),
                    BoxOffice = table.Column<decimal>(type: "numeric", nullable: true),
                    Production = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.imdbID);
                    table.ForeignKey(
                        name: "FK_Movies_Countries_CountryAlpha2",
                        column: x => x.CountryAlpha2,
                        principalTable: "Countries",
                        principalColumn: "Alpha2");
                    table.ForeignKey(
                        name: "FK_Movies_Types_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Types",
                        principalColumn: "TypeID");
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Source = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    MovieimdbID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_Rating_Movies_MovieimdbID",
                        column: x => x.MovieimdbID,
                        principalTable: "Movies",
                        principalColumn: "imdbID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CountryAlpha2",
                table: "Movies",
                column: "CountryAlpha2");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_TypeID",
                table: "Movies",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_MovieimdbID",
                table: "Rating",
                column: "MovieimdbID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
