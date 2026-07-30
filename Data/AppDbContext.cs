using Microsoft.EntityFrameworkCore;
using StudyTracker.Api.Models;

namespace StudyTracker.Api.Data;

/// <summary>
/// DbContext: EF Core object that represents the database session.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<LearningTopic> LearningTopics { get; set; } = null!;

    public DbSet<StudySession> StudySessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LearningTopic>(entity =>
        {
            entity.Property(topic => topic.Title)
                .IsRequired()
                .HasMaxLength(120);

            entity.Property(topic => topic.Description)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(topic => topic.Difficulty)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(topic => topic.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.HasMany(topic => topic.StudySessions)
                .WithOne(session => session.LearningTopic)
                .HasForeignKey(session => session.LearningTopicId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new LearningTopic
                {
                    Id = 1,
                    Title = "ASP.NET Core Web API",
                    Description = "Practice endpoints, HTTP results, and service calls.",
                    Difficulty = TopicDifficulty.Medium,
                    Status = TopicStatus.InProgress,
                    CreatedAt = new DateTime(2026, 7, 21, 0, 0, 0, DateTimeKind.Utc)
                },
                new LearningTopic
                {
                    Id = 2,
                    Title = "EF Core",
                    Description = "Practice DbContext, DbSet, LINQ, migrations, and async queries.",
                    Difficulty = TopicDifficulty.Medium,
                    Status = TopicStatus.NotStarted,
                    CreatedAt = new DateTime(2026, 7, 21, 0, 0, 0, DateTimeKind.Utc)
                },
                new LearningTopic
                {
                    Id = 3,
                    Title = "LINQ and IQueryable",
                    Description = "Practice building database queries step by step before executing them.",
                    Difficulty = TopicDifficulty.Hard,
                    Status = TopicStatus.NotStarted,
                    CreatedAt = new DateTime(2026, 7, 21, 0, 0, 0, DateTimeKind.Utc)
                });
        });

        modelBuilder.Entity<StudySession>(entity =>
        {
            entity.Property(session => session.Notes)
                .IsRequired()
                .HasMaxLength(1000);
        });
    }
}
