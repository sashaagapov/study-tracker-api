namespace StudyTracker.Api.Models;

/// <summary>
/// Represents a learning topic tracked by the API.
/// </summary>
/// <param name="Id">The unique identifier of the topic.</param>
/// <param name="Title">The display title of the topic.</param>
/// <param name="Status">The current learning status of the topic.</param>
public class LearningTopic
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public string Status { get; set; } = "";
}
