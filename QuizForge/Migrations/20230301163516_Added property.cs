using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizForge.Migrations
{
    public partial class Addedproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizType",
                table: "QuizQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizType",
                table: "QuizQuestions");
        }
    }
}
