using StudyTracker.Api.Models;

namespace StudyTracker.Api.DTOs;

public record LearningTopicDto(
    int Id,
    string Title,
    string Description,
    TopicDifficulty Difficulty,
    TopicStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int StudySessionsCount);
