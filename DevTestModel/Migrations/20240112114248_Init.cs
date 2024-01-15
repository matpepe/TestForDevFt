using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTestModel.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoldPriceModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    Metal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ask = table.Column<double>(type: "float", nullable: false),
                    Bid = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PriceGram22K = table.Column<double>(type: "float", nullable: false),
                    CH = table.Column<double>(type: "float", nullable: false),
                    CHP = table.Column<double>(type: "float", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateOfUpload = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoldPriceModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SourceInfoModel",
                columns: table => new
                {
                    SourceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceInfoModel", x => x.SourceId);
                });

            migrationBuilder.CreateTable(
                name: "NewsApiResponse",
                columns: table => new
                {
                    ApiResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsArticleArticleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsApiResponse", x => x.ApiResponseId);
                });

            migrationBuilder.CreateTable(
                name: "NewsArticle",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedOn = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Upvotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Downvotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceInfoId = table.Column<int>(type: "int", nullable: true),
                    NewsApiResponseApiResponseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticle", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_NewsArticle_NewsApiResponse_NewsApiResponseApiResponseId",
                        column: x => x.NewsApiResponseApiResponseId,
                        principalTable: "NewsApiResponse",
                        principalColumn: "ApiResponseId");
                    table.ForeignKey(
                        name: "FK_NewsArticle_SourceInfoModel_SourceInfoId",
                        column: x => x.SourceInfoId,
                        principalTable: "SourceInfoModel",
                        principalColumn: "SourceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsApiResponse_NewsArticleArticleId",
                table: "NewsApiResponse",
                column: "NewsArticleArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticle_NewsApiResponseApiResponseId",
                table: "NewsArticle",
                column: "NewsApiResponseApiResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticle_SourceInfoId",
                table: "NewsArticle",
                column: "SourceInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsApiResponse_NewsArticle_NewsArticleArticleId",
                table: "NewsApiResponse",
                column: "NewsArticleArticleId",
                principalTable: "NewsArticle",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsApiResponse_NewsArticle_NewsArticleArticleId",
                table: "NewsApiResponse");

            migrationBuilder.DropTable(
                name: "GoldPriceModel");

            migrationBuilder.DropTable(
                name: "NewsArticle");

            migrationBuilder.DropTable(
                name: "NewsApiResponse");

            migrationBuilder.DropTable(
                name: "SourceInfoModel");
        }
    }
}
