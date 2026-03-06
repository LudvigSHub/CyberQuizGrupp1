using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberQuizGrupp1.DAL.Migrations
{
    /// <inheritdoc />
    public partial class QuizRelationer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_AttemptId",
                table: "UserAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_AttemptId_QuestionId",
                table: "UserAnswers",
                columns: new[] { "AttemptId", "QuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_SelectedAnswerOptionId",
                table: "UserAnswers",
                column: "SelectedAnswerOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_SubCategoryId",
                table: "QuizAttempts",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizAttempts_SubCategories_SubCategoryId",
                table: "QuizAttempts",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_AnswerOptions_SelectedAnswerOptionId",
                table: "UserAnswers",
                column: "SelectedAnswerOptionId",
                principalTable: "AnswerOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizAttempts_SubCategories_SubCategoryId",
                table: "QuizAttempts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_AnswerOptions_SelectedAnswerOptionId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_AttemptId_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_SelectedAnswerOptionId",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuizAttempts_SubCategoryId",
                table: "QuizAttempts");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_AttemptId",
                table: "UserAnswers",
                column: "AttemptId");
        }
    }
}
