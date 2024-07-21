using Microsoft.EntityFrameworkCore;

namespace SpeakersService.Data
{
    public class SpeakerDbContext : DbContext
    {
        public DbSet<SpeakerRecord> Speakers { get; set; }

        public SpeakerDbContext(DbContextOptions<SpeakerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the SpeakerRecord entity
            modelBuilder.Entity<SpeakerRecord>()
                .HasKey(s => s.Id);

            // Add any additional configuration for the Speaker entity

            base.OnModelCreating(modelBuilder);
        }
    }
}