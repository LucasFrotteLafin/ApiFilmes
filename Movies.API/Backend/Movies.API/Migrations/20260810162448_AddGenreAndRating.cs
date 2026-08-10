using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Movies.API.Migrations
{
    public partial class AddGenreAndRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Movies",
                type: "VARCHAR",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Movies",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Movies");
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movies");
        }
    }
}