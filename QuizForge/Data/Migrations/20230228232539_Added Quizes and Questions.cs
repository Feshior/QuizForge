using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizForge.Data.Migrations
{
    public partial class AddedQuizesandQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxAttempts = table.Column<int>(type: "int", nullable: false),
                    QuizImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "quizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    QuestionPoints = table.Column<double>(type: "float", nullable: false),
                    QuestionImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quizQuestions_quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userQuizzes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userQuizzes_quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quizCorrectAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizCorrectAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quizCorrectAnswers_quizQuestions_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "quizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Points = table.Column<double>(type: "float", nullable: false),
                    UserQuizId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userPoints_userQuizzes_UserQuizId",
                        column: x => x.UserQuizId,
                        principalTable: "userQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_quizCorrectAnswers_QuizQuestionId",
                table: "quizCorrectAnswers",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_quizQuestions_QuizId",
                table: "quizQuestions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_userPoints_UserQuizId",
                table: "userPoints",
                column: "UserQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_userQuizzes_ApplicationUserId",
                table: "userQuizzes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_userQuizzes_QuizId",
                table: "userQuizzes",
                column: "QuizId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quizCorrectAnswers");

            migrationBuilder.DropTable(
                name: "userPoints");

            migrationBuilder.DropTable(
                name: "quizQuestions");

            migrationBuilder.DropTable(
                name: "userQuizzes");

            migrationBuilder.DropTable(
                name: "quizzes");
        }
    }
}
