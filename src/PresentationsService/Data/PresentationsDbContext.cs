using Microsoft.EntityFrameworkCore;

namespace PresentationsService.Data
{
    public class PresentationsDbContext : DbContext
    {
        public DbSet<PresentationRecord> Presentations { get; set; }

        public PresentationsDbContext(DbContextOptions<PresentationsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the SpeakerRecord entity
            modelBuilder.Entity<PresentationRecord>()
                .HasKey(s => s.Id);

            // Add any additional configuration for the Speaker entity

            base.OnModelCreating(modelBuilder);
        }
    }
}