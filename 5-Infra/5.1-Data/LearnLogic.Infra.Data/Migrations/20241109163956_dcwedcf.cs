using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLogic.Infra.Data.Migrations
{
    public partial class dcwedcf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Question",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Question");
        }
    }
}
