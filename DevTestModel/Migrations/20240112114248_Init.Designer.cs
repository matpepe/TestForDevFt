﻿// <auto-generated />
using System;
using DevTestModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DevTestModel.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240112114248_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DevTestModel.Models.GoldPriceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("Active")
                        .HasColumnType("bit");

                    b.Property<double>("Ask")
                        .HasColumnType("float");

                    b.Property<double>("Bid")
                        .HasColumnType("float");

                    b.Property<double>("CH")
                        .HasColumnType("float");

                    b.Property<double>("CHP")
                        .HasColumnType("float");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfUpload")
                        .HasColumnType("datetime2");

                    b.Property<string>("Metal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("PriceGram22K")
                        .HasColumnType("float");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("GoldPriceModel");
                });

            modelBuilder.Entity("DevTestModel.Models.NewsApiResponse", b =>
                {
                    b.Property<int>("ApiResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApiResponseId"), 1L, 1);

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewsArticleArticleId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ApiResponseId");

                    b.HasIndex("NewsArticleArticleId");

                    b.ToTable("NewsApiResponse");
                });

            modelBuilder.Entity("DevTestModel.Models.NewsArticleModel", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleId"), 1L, 1);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Categories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Downvotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Guid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NewsApiResponseApiResponseId")
                        .HasColumnType("int");

                    b.Property<long>("PublishedOn")
                        .HasColumnType("bigint");

                    b.Property<int?>("SourceInfoId")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Upvotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArticleId");

                    b.HasIndex("NewsApiResponseApiResponseId");

                    b.HasIndex("SourceInfoId");

                    b.ToTable("NewsArticle");
                });

            modelBuilder.Entity("DevTestModel.Models.SourceInfoModel", b =>
                {
                    b.Property<int>("SourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SourceId"), 1L, 1);

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SourceId");

                    b.ToTable("SourceInfoModel");
                });

            modelBuilder.Entity("DevTestModel.Models.NewsApiResponse", b =>
                {
                    b.HasOne("DevTestModel.Models.NewsArticleModel", "NewsArticle")
                        .WithMany()
                        .HasForeignKey("NewsArticleArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NewsArticle");
                });

            modelBuilder.Entity("DevTestModel.Models.NewsArticleModel", b =>
                {
                    b.HasOne("DevTestModel.Models.NewsApiResponse", null)
                        .WithMany("Data")
                        .HasForeignKey("NewsApiResponseApiResponseId");

                    b.HasOne("DevTestModel.Models.SourceInfoModel", "SourceInfo")
                        .WithMany()
                        .HasForeignKey("SourceInfoId");

                    b.Navigation("SourceInfo");
                });

            modelBuilder.Entity("DevTestModel.Models.NewsApiResponse", b =>
                {
                    b.Navigation("Data");
                });
#pragma warning restore 612, 618
        }
    }
}
