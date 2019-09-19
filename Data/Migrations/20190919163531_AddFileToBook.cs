using Microsoft.EntityFrameworkCore.Migrations;

namespace Emison.Data.Migrations
{
    public partial class AddFileToBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "Books");
        }
    }
}
