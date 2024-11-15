using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLogic.Infra.Data.Migrations
{
    public partial class tsrterfgv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedItem",
                table: "QuestionAnswer",
                newName: "SelectedItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedItemId",
                table: "QuestionAnswer",
                newName: "SelectedItem");
        }
    }
}
