using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnLogic.Infra.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: true),
                    CodeSnippet = table.Column<string>(type: "text", nullable: true),
                    SolutionIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FirstAccess = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ExperienceLevel = table.Column<int>(type: "integer", nullable: false),
                    Punctuation = table.Column<decimal>(type: "numeric", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activated = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionItems_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_Number",
                table: "Question",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_Title",
                table: "Question",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionItems_QuestionId_IsCorrect",
                table: "QuestionItems",
                columns: new[] { "QuestionId", "IsCorrect" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionItems");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Question");
        }
    }
}
