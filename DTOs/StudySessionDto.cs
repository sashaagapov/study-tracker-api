namespace StudyTracker.Api.DTOs;

public record StudySessionDto(
    int Id,
    int LearningTopicId,
    DateTime Date,
    int DurationMinutes,
    string Notes);
