using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class InitialMigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CookieId = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    WinDateTime = table.Column<DateTime>(nullable: true),
                    GameMode = table.Column<int>(nullable: false),
                    UsedDevCommand = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<uint>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Q1 = table.Column<int>(nullable: false),
                    Q2 = table.Column<int>(nullable: false),
                    Q3 = table.Column<int>(nullable: false),
                    Q4 = table.Column<int>(nullable: false),
                    Q5 = table.Column<int>(nullable: false),
                    Q6 = table.Column<int>(nullable: false),
                    Q7 = table.Column<int>(nullable: false),
                    Q8 = table.Column<int>(nullable: false),
                    Q9 = table.Column<int>(nullable: false),
                    Q10 = table.Column<int>(nullable: false),
                    Q11 = table.Column<int>(nullable: false),
                    Q12 = table.Column<int>(nullable: false),
                    Q13 = table.Column<int>(nullable: false),
                    Q14 = table.Column<int>(nullable: false),
                    Q15 = table.Column<int>(nullable: false),
                    Q16 = table.Column<int>(nullable: false),
                    Q17 = table.Column<int>(nullable: false),
                    Q18 = table.Column<int>(nullable: false),
                    Q19 = table.Column<int>(nullable: false),
                    Q20 = table.Column<int>(nullable: false),
                    Q21 = table.Column<int>(nullable: false),
                    Q22 = table.Column<int>(nullable: false),
                    Q23 = table.Column<int>(nullable: false),
                    Q24 = table.Column<int>(nullable: false),
                    Q25 = table.Column<int>(nullable: false),
                    Q26 = table.Column<int>(nullable: false),
                    Q27 = table.Column<int>(nullable: false),
                    Q28 = table.Column<int>(nullable: false),
                    Q29 = table.Column<int>(nullable: false),
                    Q30 = table.Column<int>(nullable: false),
                    Q31 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transcripts",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Direction = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    UserId = table.Column<uint>(nullable: true),
                    BotId = table.Column<uint>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transcripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transcripts_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transcripts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1u, "Reggie" },
                    { 2u, "Cow" },
                    { 3u, "Olive" },
                    { 4u, "Clarence" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_UserId",
                table: "Surveys",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_BotId",
                table: "Transcripts",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_Transcripts_UserId",
                table: "Transcripts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Transcripts");

            migrationBuilder.DropTable(
                name: "Bots");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
