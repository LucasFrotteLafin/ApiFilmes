using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Movies.API.Migrations
{
    public partial class AddRoleToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "users",
                type: "VARCHAR",
                maxLength: 20,
                nullable: false,
                defaultValue: "User");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "users");
        }
    }
}