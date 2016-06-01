using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnWordsFast.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    TranslationText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    MainLanguageId = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    TrainingLanguageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Languages_MainLanguageId",
                        column: x => x.MainLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Languages_TrainingLanguageId",
                        column: x => x.TrainingLanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAdditionalLanguage",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdditionalLanguage", x => new { x.UserId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_UserAdditionalLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAdditionalLanguage_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddedDateTime = table.Column<DateTime>(nullable: false),
                    Context = table.Column<string>(nullable: true),
                    LanguageId = table.Column<Guid>(nullable: false),
                    Original = table.Column<string>(nullable: true),
                    TranslationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Words_Translations_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Words_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    IsCorrect = table.Column<bool>(nullable: false),
                    Score = table.Column<float>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    WordId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingHistory_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WordAdditionalTranslation",
                columns: table => new
                {
                    WordId = table.Column<Guid>(nullable: false),
                    TranslationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordAdditionalTranslation", x => new { x.WordId, x.TranslationId });
                    table.ForeignKey(
                        name: "FK_WordAdditionalTranslation_Translations_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordAdditionalTranslation_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingHistory_WordId",
                table: "TrainingHistory",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_LanguageId",
                table: "Translations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MainLanguageId",
                table: "Users",
                column: "MainLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TrainingLanguageId",
                table: "Users",
                column: "TrainingLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalLanguage_LanguageId",
                table: "UserAdditionalLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdditionalLanguage_UserId",
                table: "UserAdditionalLanguage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_LanguageId",
                table: "Words",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_TranslationId",
                table: "Words",
                column: "TranslationId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_UserId",
                table: "Words",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WordAdditionalTranslation_TranslationId",
                table: "WordAdditionalTranslation",
                column: "TranslationId");

            migrationBuilder.CreateIndex(
                name: "IX_WordAdditionalTranslation_WordId",
                table: "WordAdditionalTranslation",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingHistory");

            migrationBuilder.DropTable(
                name: "UserAdditionalLanguage");

            migrationBuilder.DropTable(
                name: "WordAdditionalTranslation");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
