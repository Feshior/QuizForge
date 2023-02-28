using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizForge.Data.Migrations
{
    public partial class ModifiedApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VisibleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisibleName",
                table: "AspNetUsers");
        }
    }
}
