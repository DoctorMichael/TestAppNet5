using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApp.DataAccess.Migrations
{
    public partial class SeedMany2Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "QuestionTest",
                columns: new[] { "QuestionsId", "TestsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "QuestionTest",
                keyColumns: new[] { "QuestionsId", "TestsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "QuestionTest",
                keyColumns: new[] { "QuestionsId", "TestsId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "QuestionTest",
                keyColumns: new[] { "QuestionsId", "TestsId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "QuestionTest",
                keyColumns: new[] { "QuestionsId", "TestsId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "QuestionTest",
                keyColumns: new[] { "QuestionsId", "TestsId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "QuestionTest",
                keyColumns: new[] { "QuestionsId", "TestsId" },
                keyValues: new object[] { 6, 3 });
        }
    }
}
