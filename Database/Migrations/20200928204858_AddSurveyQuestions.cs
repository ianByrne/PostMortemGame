using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddSurveyQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 1u,
                column: "Type",
                value: 7);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 2u,
                column: "Type",
                value: 8);

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "Question", "Type" },
                values: new object[,]
                {
                    { 33u, "Would you like to play the game again?", 5 },
                    { 32u, "When interrupted, were you disappointed that the game was over?", 1 },
                    { 31u, "How much would you say you enjoyed playing the game?", 0 },
                    { 30u, "To what extent did you enjoy the graphics and the imagery?", 0 },
                    { 29u, "At any point did you find yourself become so involved that you wanted to speak to the game directly?", 1 },
                    { 28u, "Were you in suspense about whether or not you would win or lose the game?", 1 },
                    { 27u, "How much did you want to \"win\" the game?", 1 },
                    { 26u, "To what extent were you interested in seeing how the game's events would progress?", 0 },
                    { 25u, "To what extent did you feel emotionally attached to the game?", 1 },
                    { 24u, "How well do you think you performed in the game?", 4 },
                    { 23u, "To what extent did you feel like you were making progress towards the end of the game?", 0 },
                    { 22u, "To what extent did you find the game easy?", 1 },
                    { 21u, "To what extent did you feel motivated while playing?", 0 },
                    { 20u, "Were there any times during the game in which you just wanted to give up?", 0 },
                    { 19u, "To what extent did you find the game challenging?", 3 },
                    { 17u, "At any point did you find yourself become so involved that you were unaware you were even using controls?", 1 },
                    { 16u, "To what extent was your sense of being in the game environment stronger than your sense of being in the real world?", 1 },
                    { 15u, "To what extent did you feel that the game was something you were experiencing, rather than something you were just doing?", 1 },
                    { 14u, "To what extent did you feel as though you were separated from your real-world environment?", 1 },
                    { 13u, "To what extent did you feel that you were interacting with the game environment?", 1 },
                    { 12u, "Did you feel the urge at any point to stop playing and see what was happening around you?", 1 },
                    { 11u, "To what extent did you notice events taking place around you?", 0 },
                    { 10u, "To what extent were you aware of yourself in your surroundings?", 2 },
                    { 9u, "To what extent did you forget about your everyday concerns?", 0 },
                    { 8u, "To what extent did you feel consciously aware of being in the real world whilst playing?", 1 },
                    { 7u, "To what extent did you lose track of time?", 0 },
                    { 6u, "Did you feel that you were trying you best?", 1 },
                    { 5u, "How much effort did you put into playing the game?", 0 },
                    { 4u, "To what extent did you feel you were focused on the game?", 0 },
                    { 18u, "To what extent did you feel as though you were moving through the game according to you own will?", 1 },
                    { 3u, "To what extent did the game hold your attention?", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 3u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 4u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 5u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 6u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 7u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 8u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 9u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 10u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 11u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 12u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 13u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 14u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 15u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 16u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 17u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 18u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 19u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 20u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 21u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 22u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 23u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 24u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 25u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 26u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 27u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 28u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 29u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 30u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 31u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 32u);

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 33u);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 1u,
                column: "Type",
                value: 2);

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: 2u,
                column: "Type",
                value: 3);
        }
    }
}
