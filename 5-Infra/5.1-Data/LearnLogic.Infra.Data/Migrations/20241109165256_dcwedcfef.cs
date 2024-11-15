using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLogic.Infra.Data.Migrations
{
    public partial class dcwedcfef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Question");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "QuestionAnswer",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "QuestionAnswer");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Question",
                type: "text",
                nullable: true);
        }
    }
}
