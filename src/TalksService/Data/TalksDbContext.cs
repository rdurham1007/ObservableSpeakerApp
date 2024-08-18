using Microsoft.EntityFrameworkCore;

namespace TalksService.Data
{
    public class TalksDbContext : DbContext
    {
        public DbSet<TalkRecord> Talks { get; set; }

        public TalksDbContext(DbContextOptions<TalksDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the SpeakerRecord entity
            modelBuilder.Entity<TalkRecord>()
                .HasKey(s => s.Id);

            // Add any additional configuration for the Speaker entity

            base.OnModelCreating(modelBuilder);
        }
    }
}