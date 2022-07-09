using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_video_center.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gmm7");

            migrationBuilder.CreateTable(
                name: "medias",
                schema: "gmm7",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Path = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    MediaType = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')"),
                    LastModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')"),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    IconUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 2083, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "gmm7",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')"),
                    LastModifyTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaTags",
                schema: "gmm7",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MediaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTags", x => new { x.TagId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_MediaTags_medias_MediaId",
                        column: x => x.MediaId,
                        principalSchema: "gmm7",
                        principalTable: "medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MediaTags_tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "gmm7",
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_medias_Id_Name_MediaType_Description",
                schema: "gmm7",
                table: "medias",
                columns: new[] { "Id", "Name", "MediaType", "Description" });

            migrationBuilder.CreateIndex(
                name: "IX_MediaTags_MediaId",
                schema: "gmm7",
                table: "MediaTags",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_Id_Name",
                schema: "gmm7",
                table: "tags",
                columns: new[] { "Id", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaTags",
                schema: "gmm7");

            migrationBuilder.DropTable(
                name: "medias",
                schema: "gmm7");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "gmm7");
        }
    }
}
