using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class UpdateBotNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 1u,
                column: "Name",
                value: "Reggie");

            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2u, "Cow" },
                    { 3u, "Olive" },
                    { 4u, "Clarence" }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 1u,
                columns: new[] { "Question", "Type" },
                values: new object[] { "Age", 2 });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "Question", "Type" },
                values: new object[] { 2u, "Gender", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 2u);

            migrationBuilder.DeleteData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 3u);

            migrationBuilder.DeleteData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 4u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 2u);

            migrationBuilder.UpdateData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 1u,
                column: "Name",
                value: "Paul");

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 1u,
                columns: new[] { "Question", "Type" },
                values: new object[] { "What is your favourite colour?", 1 });
        }
    }
}
