namespace StudyTracker.Api.DTOs;

public record CreateStudySessionDto(
    int LearningTopicId,
    DateTime Date,
    int DurationMinutes,
    string Notes);
