using GMM7.Models;
using Microsoft.EntityFrameworkCore;

namespace GMM7
{
    public class DefaultDBContext : IDBContext
    {
        private readonly string DateTimeSqlValue = "datetime('now')";
        private const string DefaultDataBaseName = "DefaultConnection";
        private readonly IConfiguration Configuration;

        private readonly ILoggerFactory LoggerFactory;

        public DefaultDBContext(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            LoggerFactory = loggerFactory;
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(LoggerFactory)
            .UseSqlite(Configuration.GetConnectionString(DefaultDataBaseName));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MediaTag>()
                      .HasKey(t => new { t.TagId, t.MediaId });

            modelBuilder.Entity<MediaTag>()
                .HasOne(pt => pt.Media)
                .WithMany(p => p.MediaTags)
                .HasForeignKey(pt => pt.MediaId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<MediaTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.MediaTags)
                .HasForeignKey(pt => pt.TagId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<MediaTag>()
                .Property(p => p.PublicationDate)
                .HasDefaultValueSql(DateTimeSqlValue)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Tag>()
                .Property(p => p.CreateTime)
                .HasDefaultValueSql(DateTimeSqlValue)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Tag>()
                .Property(p => p.LastModifyTime)
                .HasDefaultValueSql(DateTimeSqlValue)
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Media>()
                .Property(p => p.CreateTime)
                .HasDefaultValueSql(DateTimeSqlValue)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Media>()
                .Property(p => p.LastModifyTime)
                .HasDefaultValueSql(DateTimeSqlValue)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}