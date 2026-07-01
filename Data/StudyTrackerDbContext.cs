using Microsoft.EntityFrameworkCore;
using StudyTracker.Api.Models;

namespace StudyTracker.Api.Data;

public class StudyTrackerDbContext : DbContext
{
    public StudyTrackerDbContext(DbContextOptions<StudyTrackerDbContext> options)
        : base(options)
    {
    }

    public DbSet<LearningTopic> LearningTopics { get; set; } = null!;
}
