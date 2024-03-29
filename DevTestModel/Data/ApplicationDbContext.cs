﻿
using DevTestModel.Models;
using DevTestModel.Models.DC_models;
using Microsoft.EntityFrameworkCore;

namespace DevTestModel.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() { }

        #region DbSet
        //public DbSet<Product> Product { get; set; }
        public DbSet<NewsArticleModel> NewsArticle { get; set; }
        //public DbSet<GoldPriceModel> GoldPriceModel { get; set; }
        public DbSet<NewsApiResponse> NewsApiResponse { get; set; }
        public DbSet<SourceInfoModel> SourceInfoModel { get; set; }
        public DbSet<DC_NewsCategoryCR> DC_NewsCategoryCR { get; set; }
        public DbSet<DataHistoryArticle> DataHistoryArticle { get; set; }

        //public DbSet<MailClient> MailClient { get; set; }

        ////public DbSet<MainViewWrapperModel> MainViewWrapperModel { get; set; } //remove
        //public DbSet<SourceInfoModel> SourceInfoModel { get; set; }
        // dodat 3 klase , bindat ih ili insertat podatkle u bazu i retrieve , nadopuniti index da pokaze info za JedanKreirani acc
        // sve ljepo dokupentirat + stavke sa mobitela + nesto... 
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-N89DPOR\\SQLEXPRESS;Database=TestDevALL;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");
            }
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}