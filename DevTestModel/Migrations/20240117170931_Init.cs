﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTestModel.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DC_NewsCategoryCR",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryNewsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_NewsCategoryCR", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "DataHistoryArticle",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedOn = table.Column<long>(type: "bigint", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Upvotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Downvotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceInfoId = table.Column<int>(type: "int", nullable: true),
                    NewsApiResponseApiResponseId = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateAndTimeInserted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataHistoryArticle", x => x.ArticleId);
                });

            migrationBuilder.CreateTable(
                name: "NewsApiResponse",
                columns: table => new
                {
                    ApiResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsArticleArticleId = table.Column<int>(type: "int", nullable: true)
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
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceInfoModel", x => x.SourceId);
                    table.ForeignKey(
                        name: "FK_SourceInfoModel_NewsArticle_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "NewsArticle",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataHistoryArticle_SourceInfoId",
                table: "DataHistoryArticle",
                column: "SourceInfoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_SourceInfoModel_ArticleId",
                table: "SourceInfoModel",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataHistoryArticle_SourceInfoModel_SourceInfoId",
                table: "DataHistoryArticle",
                column: "SourceInfoId",
                principalTable: "SourceInfoModel",
                principalColumn: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsApiResponse_NewsArticle_NewsArticleArticleId",
                table: "NewsApiResponse",
                column: "NewsArticleArticleId",
                principalTable: "NewsArticle",
                principalColumn: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticle_SourceInfoModel_SourceInfoId",
                table: "NewsArticle",
                column: "SourceInfoId",
                principalTable: "SourceInfoModel",
                principalColumn: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticle_SourceInfoModel_SourceInfoId",
                table: "NewsArticle");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsApiResponse_NewsArticle_NewsArticleArticleId",
                table: "NewsApiResponse");

            migrationBuilder.DropTable(
                name: "DataHistoryArticle");

            migrationBuilder.DropTable(
                name: "DC_NewsCategoryCR");

            migrationBuilder.DropTable(
                name: "SourceInfoModel");

            migrationBuilder.DropTable(
                name: "NewsArticle");

            migrationBuilder.DropTable(
                name: "NewsApiResponse");
        }
    }
}
