using GMM7.Models;
using Microsoft.EntityFrameworkCore;

namespace GMM7
{
    public abstract class IDBContext : DbContext
    {
        public DbSet<Media> Medias { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<MediaTag> MediaTags { set; get; }
    }
}