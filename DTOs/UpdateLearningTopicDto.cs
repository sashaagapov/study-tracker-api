using StudyTracker.Api.Models;

namespace StudyTracker.Api.DTOs;

public record UpdateLearningTopicDto(
    string Title,
    string Description,
    TopicDifficulty Difficulty,
    TopicStatus Status);
