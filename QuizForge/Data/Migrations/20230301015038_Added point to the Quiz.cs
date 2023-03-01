using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizForge.Data.Migrations
{
    public partial class AddedpointtotheQuiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizPoint",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizPoint",
                table: "Quizzes");
        }
    }
}
