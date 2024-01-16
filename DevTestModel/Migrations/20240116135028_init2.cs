using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTestModel.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsApiResponse_NewsArticle_NewsArticleArticleId",
                table: "NewsApiResponse");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "NewsApiResponse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NewsArticleArticleId",
                table: "NewsApiResponse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "NewsApiResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "NewsApiResponse",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsApiResponse_NewsArticle_NewsArticleArticleId",
                table: "NewsApiResponse",
                column: "NewsArticleArticleId",
                principalTable: "NewsArticle",
                principalColumn: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsApiResponse_NewsArticle_NewsArticleArticleId",
                table: "NewsApiResponse");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "NewsApiResponse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NewsArticleArticleId",
                table: "NewsApiResponse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "NewsApiResponse",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "NewsApiResponse",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsApiResponse_NewsArticle_NewsArticleArticleId",
                table: "NewsApiResponse",
                column: "NewsArticleArticleId",
                principalTable: "NewsArticle",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
