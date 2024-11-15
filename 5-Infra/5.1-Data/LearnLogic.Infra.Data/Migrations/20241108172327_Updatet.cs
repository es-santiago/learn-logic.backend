using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLogic.Infra.Data.Migrations
{
    public partial class Updatet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "QuestionItems",
                newName: "Label");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Label",
                table: "QuestionItems",
                newName: "Content");
        }
    }
}
