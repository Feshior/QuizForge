using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizForge.Data.Migrations
{
    public partial class smallchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quizCorrectAnswers_quizQuestions_QuizQuestionId",
                table: "quizCorrectAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_quizQuestions_quizzes_QuizId",
                table: "quizQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_userPoints_userQuizzes_UserQuizId",
                table: "userPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_userQuizzes_AspNetUsers_ApplicationUserId",
                table: "userQuizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_userQuizzes_quizzes_QuizId",
                table: "userQuizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userQuizzes",
                table: "userQuizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userPoints",
                table: "userPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quizzes",
                table: "quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quizQuestions",
                table: "quizQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quizCorrectAnswers",
                table: "quizCorrectAnswers");

            migrationBuilder.RenameTable(
                name: "userQuizzes",
                newName: "UserQuizzes");

            migrationBuilder.RenameTable(
                name: "userPoints",
                newName: "UserPoints");

            migrationBuilder.RenameTable(
                name: "quizzes",
                newName: "Quizzes");

            migrationBuilder.RenameTable(
                name: "quizQuestions",
                newName: "QuizQuestions");

            migrationBuilder.RenameTable(
                name: "quizCorrectAnswers",
                newName: "QuizCorrectAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_userQuizzes_QuizId",
                table: "UserQuizzes",
                newName: "IX_UserQuizzes_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_userQuizzes_ApplicationUserId",
                table: "UserQuizzes",
                newName: "IX_UserQuizzes_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_userPoints_UserQuizId",
                table: "UserPoints",
                newName: "IX_UserPoints_UserQuizId");

            migrationBuilder.RenameIndex(
                name: "IX_quizQuestions_QuizId",
                table: "QuizQuestions",
                newName: "IX_QuizQuestions_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_quizCorrectAnswers_QuizQuestionId",
                table: "QuizCorrectAnswers",
                newName: "IX_QuizCorrectAnswers_QuizQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuizzes",
                table: "UserQuizzes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPoints",
                table: "UserPoints",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizCorrectAnswers",
                table: "QuizCorrectAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizCorrectAnswers_QuizQuestions_QuizQuestionId",
                table: "QuizCorrectAnswers",
                column: "QuizQuestionId",
                principalTable: "QuizQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_Quizzes_QuizId",
                table: "QuizQuestions",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPoints_UserQuizzes_UserQuizId",
                table: "UserPoints",
                column: "UserQuizId",
                principalTable: "UserQuizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuizzes_AspNetUsers_ApplicationUserId",
                table: "UserQuizzes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuizzes_Quizzes_QuizId",
                table: "UserQuizzes",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizCorrectAnswers_QuizQuestions_QuizQuestionId",
                table: "QuizCorrectAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_Quizzes_QuizId",
                table: "QuizQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPoints_UserQuizzes_UserQuizId",
                table: "UserPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuizzes_AspNetUsers_ApplicationUserId",
                table: "UserQuizzes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuizzes_Quizzes_QuizId",
                table: "UserQuizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuizzes",
                table: "UserQuizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPoints",
                table: "UserPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quizzes",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizCorrectAnswers",
                table: "QuizCorrectAnswers");

            migrationBuilder.RenameTable(
                name: "UserQuizzes",
                newName: "userQuizzes");

            migrationBuilder.RenameTable(
                name: "UserPoints",
                newName: "userPoints");

            migrationBuilder.RenameTable(
                name: "Quizzes",
                newName: "quizzes");

            migrationBuilder.RenameTable(
                name: "QuizQuestions",
                newName: "quizQuestions");

            migrationBuilder.RenameTable(
                name: "QuizCorrectAnswers",
                newName: "quizCorrectAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuizzes_QuizId",
                table: "userQuizzes",
                newName: "IX_userQuizzes_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuizzes_ApplicationUserId",
                table: "userQuizzes",
                newName: "IX_userQuizzes_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPoints_UserQuizId",
                table: "userPoints",
                newName: "IX_userPoints_UserQuizId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "quizQuestions",
                newName: "IX_quizQuestions_QuizId");

            migrationBuilder.RenameIndex(
                name: "IX_QuizCorrectAnswers_QuizQuestionId",
                table: "quizCorrectAnswers",
                newName: "IX_quizCorrectAnswers_QuizQuestionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userQuizzes",
                table: "userQuizzes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userPoints",
                table: "userPoints",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quizzes",
                table: "quizzes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quizQuestions",
                table: "quizQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quizCorrectAnswers",
                table: "quizCorrectAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_quizCorrectAnswers_quizQuestions_QuizQuestionId",
                table: "quizCorrectAnswers",
                column: "QuizQuestionId",
                principalTable: "quizQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_quizQuestions_quizzes_QuizId",
                table: "quizQuestions",
                column: "QuizId",
                principalTable: "quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userPoints_userQuizzes_UserQuizId",
                table: "userPoints",
                column: "UserQuizId",
                principalTable: "userQuizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userQuizzes_AspNetUsers_ApplicationUserId",
                table: "userQuizzes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userQuizzes_quizzes_QuizId",
                table: "userQuizzes",
                column: "QuizId",
                principalTable: "quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
