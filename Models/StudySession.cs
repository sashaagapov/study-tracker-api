namespace StudyTracker.Api.Models;

/// <summary>
/// Entity: represents one study session connected to a learning topic.
/// </summary>
public class StudySession
{
    public int Id { get; set; }

    public int LearningTopicId { get; set; }

    public LearningTopic LearningTopic { get; set; } = null!;

    public DateTime Date { get; set; }

    public int DurationMinutes { get; set; }

    public string Notes { get; set; } = "";
}
