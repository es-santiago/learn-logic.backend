using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLogic.Infra.Data.Migrations
{
    public partial class wefwefcerer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Question",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Question");
        }
    }
}
