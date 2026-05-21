namespace StudyTracker.Api.DTOs;

/// <summary>
/// Represents the data required to create a new learning topic.
/// </summary>
/// <param name="Title">The title of the topic to create.</param>
/// <param name="Status">The initial status of the topic.</param>
public record CreateLearningTopicRequest(string Title, string Status);
