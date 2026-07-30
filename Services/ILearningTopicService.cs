using StudyTracker.Api.DTOs;

namespace StudyTracker.Api.Services;

public interface ILearningTopicService
{
    Task<List<LearningTopicDto>> GetAllAsync(string? status, string? difficulty, string? search);

    Task<LearningTopicDto?> GetByIdAsync(int id);

    Task<LearningTopicDto> CreateAsync(CreateLearningTopicDto dto);

    Task<bool> UpdateAsync(int id, UpdateLearningTopicDto dto);

    Task<bool> DeleteAsync(int id);
}
