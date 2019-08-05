using Microsoft.EntityFrameworkCore.Migrations;

namespace SpigotCaseStudy.Migrations
{
    public partial class AddedMediaTypeProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "MediaItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "MediaItems");
        }
    }
}
