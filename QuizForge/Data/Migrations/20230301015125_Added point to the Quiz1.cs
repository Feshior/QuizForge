using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizForge.Data.Migrations
{
    public partial class AddedpointtotheQuiz1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuizPoint",
                table: "Quizzes",
                newName: "QuizPoints");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuizPoints",
                table: "Quizzes",
                newName: "QuizPoint");
        }
    }
}
