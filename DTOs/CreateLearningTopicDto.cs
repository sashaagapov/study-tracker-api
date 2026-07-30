using StudyTracker.Api.Models;

namespace StudyTracker.Api.DTOs;

public record CreateLearningTopicDto(
    string Title,
    string Description,
    TopicDifficulty Difficulty);
