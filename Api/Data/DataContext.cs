using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<AppUsers> users { get; set; }

        public DbSet<UserLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<UserLike>()
            .HasKey(k=>new {k.SourceUesrId,k.LikedUserId});

             builder.Entity<UserLike>()
             .HasOne(s=>s.SourceUser) //userLike
             .WithMany(l=>l.LikedUsers) //in appuser
             .HasForeignKey(s =>s.SourceUesrId)//foren key
             .OnDelete(DeleteBehavior.Cascade);

              builder.Entity<UserLike>()
             .HasOne(s=>s.LikedUser) //userlike
             .WithMany(l=>l.LikedByUsers) //in appuser
             .HasForeignKey(s =>s.LikedUserId)// forenkey
             .OnDelete(DeleteBehavior.Cascade);

             
        }
    }
}