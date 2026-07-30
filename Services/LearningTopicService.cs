using StudyTracker.Api.DTOs;
using StudyTracker.Api.Data;
using StudyTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace StudyTracker.Api.Services;

/// <summary>
/// Service: keeps learning-topic business logic out of HTTP endpoints.
/// </summary>
public class LearningTopicService : ILearningTopicService
{
    private readonly AppDbContext _context;

    public LearningTopicService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LearningTopicDto>> GetAllAsync(string? status, string? difficulty, string? search)
    {
        IQueryable<LearningTopic> query = _context.LearningTopics
            .Include(topic => topic.StudySessions);

        if (!string.IsNullOrWhiteSpace(status))
        {
            if (Enum.TryParse(status, ignoreCase: true, out TopicStatus parsedStatus))
            {
                query = query.Where(topic => topic.Status == parsedStatus);
            }
            else
            {
                query = query.Where(topic => false);
            }
        }

        if (!string.IsNullOrWhiteSpace(difficulty))
        {
            if (Enum.TryParse(difficulty, ignoreCase: true, out TopicDifficulty parsedDifficulty))
            {
                query = query.Where(topic => topic.Difficulty == parsedDifficulty);
            }
            else
            {
                query = query.Where(topic => false);
            }
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(topic => topic.Title.Contains(search));
        }

        List<LearningTopic> topics = await query
            .OrderBy(topic => topic.Id)
            .ToListAsync();

        return topics.Select(MapToDto).ToList();
    }

    public async Task<LearningTopicDto?> GetByIdAsync(int id)
    {
        LearningTopic? topic = await _context.LearningTopics
            .Include(topic => topic.StudySessions)
            .FirstOrDefaultAsync(topic => topic.Id == id);

        return topic is null ? null : MapToDto(topic);
    }

    public async Task<LearningTopicDto> CreateAsync(CreateLearningTopicDto dto)
    {
        LearningTopic topic = new()
        {
            Title = dto.Title.Trim(),
            Description = dto.Description.Trim(),
            Difficulty = dto.Difficulty,
            Status = TopicStatus.NotStarted,
            CreatedAt = DateTime.UtcNow
        };

        _context.LearningTopics.Add(topic);
        await _context.SaveChangesAsync();

        return MapToDto(topic);
    }

    public async Task<bool> UpdateAsync(int id, UpdateLearningTopicDto dto)
    {
        LearningTopic? topic = await _context.LearningTopics
            .FirstOrDefaultAsync(topic => topic.Id == id);

        if (topic is null)
        {
            return false;
        }

        topic.Title = dto.Title.Trim();
        topic.Description = dto.Description.Trim();
        topic.Difficulty = dto.Difficulty;
        topic.Status = dto.Status;
        topic.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        LearningTopic? topic = await _context.LearningTopics
            .FirstOrDefaultAsync(topic => topic.Id == id);

        if (topic is null)
        {
            return false;
        }

        _context.LearningTopics.Remove(topic);
        await _context.SaveChangesAsync();

        return true;
    }

    private static LearningTopicDto MapToDto(LearningTopic topic)
    {
        return new LearningTopicDto(
            topic.Id,
            topic.Title,
            topic.Description,
            topic.Difficulty,
            topic.Status,
            topic.CreatedAt,
            topic.UpdatedAt,
            topic.StudySessions.Count);
    }
}
