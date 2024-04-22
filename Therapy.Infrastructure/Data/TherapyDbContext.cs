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
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Exercise>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.Media)
                .WithOne(m => m.Exercise)
                .HasForeignKey(m => m.ExerciseId);

            modelBuilder.Entity<WorkoutExercise>()
                .HasKey(we => new { we.WorkoutId, we.ExerciseId });

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Workout)
                .WithMany(w => w.WorkoutExercises)
                .HasForeignKey(we => we.WorkoutId);

            modelBuilder.Entity<WorkoutExercise>()
                .HasOne(we => we.Exercise)
                .WithMany(e => e.WorkoutExercises)
                .HasForeignKey(we => we.ExerciseId);

            modelBuilder.Entity<Workout>()
                .Property(w => w.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserType)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.UserTypeId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.RefreshToken)
                .WithOne(r => r.User)
                .HasForeignKey<RefreshToken>(r => r.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
                
            base.OnModelCreating(modelBuilder);
        }
    }
}