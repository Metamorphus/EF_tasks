using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Week7_2.Migrations
{
    public partial class UMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AddedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "DATE()"),
                    Content = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UrlName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    NavLinkId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    PageId = table.Column<int>(nullable: false),
                    ParentPageId = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.NavLinkId);
                    table.ForeignKey(
                        name: "FK_Links_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Links_Pages_ParentPageId",
                        column: x => x.ParentPageId,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatedPages",
                columns: table => new
                {
                    RelationId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    FirstPageId = table.Column<int>(nullable: false),
                    SecondPageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPages", x => x.RelationId);
                    table.ForeignKey(
                        name: "FK_RelatedPages_Pages_FirstPageId",
                        column: x => x.FirstPageId,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatedPages_Pages_SecondPageId",
                        column: x => x.SecondPageId,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_PageId",
                table: "Links",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ParentPageId",
                table: "Links",
                column: "ParentPageId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPages_FirstPageId",
                table: "RelatedPages",
                column: "FirstPageId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPages_SecondPageId",
                table: "RelatedPages",
                column: "SecondPageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "RelatedPages");

            migrationBuilder.DropTable(
                name: "Pages");
        }
    }
}
