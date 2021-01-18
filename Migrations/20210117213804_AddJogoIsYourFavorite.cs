using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFavoriteThings.Migrations
{
    public partial class AddJogoIsYourFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsYourFavorite",
                table: "Jogos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsYourFavorite",
                table: "Jogos");
        }
    }
}
