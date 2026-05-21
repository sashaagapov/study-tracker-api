namespace StudyTracker.Api.DTOs;

/// <summary>
/// Represents the data required to update an existing learning topic.
/// </summary>
/// <param name="Title">The updated title of the topic.</param>
/// <param name="Status">The updated status of the topic.</param>
public record UpdateLearningTopicRequest(string Title, string Status);
