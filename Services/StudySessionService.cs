using StudyTracker.Api.Data;
using StudyTracker.Api.DTOs;
using StudyTracker.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace StudyTracker.Api.Services;


public class StudySessionService : IStudySessionsService
{
    private readonly AppDbContext _context;
    public StudySessionService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<StudySessionDto> Create(CreateStudySessionDto dto)
    {
        if (dto.DurationMinutes <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(dto.DurationMinutes), "Duration must be greater than zero.");
        }
        if (dto.Date > DateTime.UtcNow)
        {
            throw new ArgumentException(nameof(dto.Date), "Duration cannot be set in future");
        }
        bool topicExists = await _context.LearningTopics
    .AnyAsync(x => x.Id == dto.LearningTopicId);
        if (topicExists == false)
        {
            throw new KeyNotFoundException("There are no topic with this id");
        }
        StudySession session = new()
        {
            LearningTopicId = dto.LearningTopicId,
            Date = dto.Date,
            DurationMinutes = dto.DurationMinutes,
            Notes = dto.Notes
        };
        _context.StudySessions.Add(session);
        await _context.SaveChangesAsync();
        StudySessionDto studySessionDto = new(
    session.Id,
    session.LearningTopicId,
    session.Date,
    session.DurationMinutes,
    session.Notes
);
        return studySessionDto;

    }

    public async Task<List<StudySessionDto>> GetAllAsync()
    {
        List<StudySessionDto> sessions = await _context.StudySessions.Select(session => new StudySessionDto(
     session.Id,
     session.LearningTopicId,
     session.Date,
     session.DurationMinutes,
     session.Notes
 )).ToListAsync();
        return sessions;
    }
    public async Task<StudySessionDto> GetById(int id)
    {
        StudySession? session = await _context.StudySessions.FirstOrDefaultAsync(x => x.Id == id);
        if (session == null)
        {
            throw new KeyNotFoundException($"There is no session with ID: {id}");
        }
        StudySessionDto dto = new(
       session.Id,
       session.LearningTopicId,
       session.Date,
       session.DurationMinutes,
       session.Notes
   );
        return dto;
    }
    public async Task DeleteAsync (int id)
    {
        StudySession? session = await _context.StudySessions.FirstOrDefaultAsync(x => x.Id == id);
        if(session == null)
        {
            throw new KeyNotFoundException($"There are no session with ID: {id}");
        }
        _context.StudySessions.Remove(session);
        await _context.SaveChangesAsync();

    }
}