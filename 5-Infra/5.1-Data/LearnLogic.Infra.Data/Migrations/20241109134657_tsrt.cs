using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLogic.Infra.Data.Migrations
{
    public partial class tsrt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "QuestionItems");

            migrationBuilder.CreateTable(
                name: "QuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SelectedItem = table.Column<Guid>(type: "uuid", nullable: false),
                    Correct = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Activated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswer", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswer");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "QuestionItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
