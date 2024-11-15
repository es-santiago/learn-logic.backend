using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLogic.Infra.Data.Migrations
{
    public partial class wsdqdweqd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionItems_QuestionId_IsCorrect",
                table: "QuestionItems");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionItems_QuestionId",
                table: "QuestionItems",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuestionItems_QuestionId",
                table: "QuestionItems");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionItems_QuestionId_IsCorrect",
                table: "QuestionItems",
                columns: new[] { "QuestionId", "IsCorrect" },
                unique: true);
        }
    }
}
