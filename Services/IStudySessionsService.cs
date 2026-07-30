using StudyTracker.Api.DTOs;
namespace StudyTracker.Api.Services;


public interface IStudySessionsService
{
    Task<StudySessionDto> Create(CreateStudySessionDto dto);
    Task<List<StudySessionDto>> GetAllAsync();
    Task<StudySessionDto> GetById(int id);
    Task DeleteAsync(int id);
}