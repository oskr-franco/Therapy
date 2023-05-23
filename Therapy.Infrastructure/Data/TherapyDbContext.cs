using Microsoft.EntityFrameworkCore;
using Therapy.Domain.Entities;

namespace Therapy.Infrastructure.Data {
    public class TherapyDbContext : DbContext
    {
        public TherapyDbContext(DbContextOptions<TherapyDbContext> options) : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Media> Media { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.Media)
                .WithOne(m => m.Exercise)
                .HasForeignKey(m => m.ExerciseId);
                    
            base.OnModelCreating(modelBuilder);
        }
    }
}