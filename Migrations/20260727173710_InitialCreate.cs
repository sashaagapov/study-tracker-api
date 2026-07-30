using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudyTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LearningTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningTopics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudySessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearningTopicId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudySessions_LearningTopics_LearningTopicId",
                        column: x => x.LearningTopicId,
                        principalTable: "LearningTopics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LearningTopics",
                columns: new[] { "Id", "CreatedAt", "Description", "Difficulty", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Practice endpoints, HTTP results, and service calls.", "Medium", "InProgress", "ASP.NET Core Web API", null },
                    { 2, new DateTime(2026, 7, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Practice DbContext, DbSet, LINQ, migrations, and async queries.", "Medium", "NotStarted", "EF Core", null },
                    { 3, new DateTime(2026, 7, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Practice building database queries step by step before executing them.", "Hard", "NotStarted", "LINQ and IQueryable", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_LearningTopicId",
                table: "StudySessions",
                column: "LearningTopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudySessions");

            migrationBuilder.DropTable(
                name: "LearningTopics");
        }
    }
}
