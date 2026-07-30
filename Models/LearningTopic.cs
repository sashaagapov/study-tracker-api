namespace StudyTracker.Api.Models;

/// <summary>
/// Entity: represents a database table row for a learning topic.
/// </summary>
public class LearningTopic
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Description { get; set; } = "";

    public TopicDifficulty Difficulty { get; set; }

    public TopicStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public ICollection<StudySession> StudySessions { get; set; } = new List<StudySession>();
}
