using System.Text.Json.Serialization;
using StudyTracker.Api.DTOs;
using StudyTracker.Api.Services;
using StudyTracker.Api.Data;
using Microsoft.EntityFrameworkCore;
using StudyTracker.Api.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ILearningTopicService, LearningTopicService>();
builder.Services.AddScoped<IStudySessionsService, StudySessionService>();

var app = builder.Build();

// Enable Swagger/OpenAPI only during development to keep the setup beginner-friendly.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS when possible.
app.UseHttpsRedirection();

app.UseExceptionHandler();

MapLearningTopicEndpoints(app, "/api/learning-topics");

// Legacy routes from the original Minimal API project, kept so existing requests still work.
MapLearningTopicEndpoints(app, "/api/topics");
MapStudySessionEndpoints(app, "/api/study-sessions");
// Start the web application.
app.Run();

static void MapLearningTopicEndpoints(WebApplication app, string routePrefix)
{
    RouteGroupBuilder group = app.MapGroup(routePrefix);

    // GET handles reading data. Query string filters are passed to the service.
    group.MapGet("", async (string? status, string? difficulty, string? search, ILearningTopicService service) =>
    {
        List<LearningTopicDto> topics = await service.GetAllAsync(status, difficulty, search);
        return Results.Ok(topics);
    });

    // GET by id returns one topic or 404 when it does not exist.
    group.MapGet("/{id:int}", async (int id, ILearningTopicService service) =>
    {
        LearningTopicDto? topic = await service.GetByIdAsync(id);

        return topic is null
            ? Results.NotFound()
            : Results.Ok(topic);
    });

    // POST handles creation. Results.Created returns HTTP 201 plus a Location header.
    group.MapPost("", async (CreateLearningTopicDto dto, ILearningTopicService service) =>
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return Results.BadRequest("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(dto.Description))
        {
            return Results.BadRequest("Description is required.");
        }

        LearningTopicDto newTopic = await service.CreateAsync(dto);
        return Results.Created($"/api/learning-topics/{newTopic.Id}", newTopic);
    });

    // PUT handles replacing/updating an existing resource.
    group.MapPut("/{id:int}", async (int id, UpdateLearningTopicDto dto, ILearningTopicService service) =>
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            return Results.BadRequest("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(dto.Description))
        {
            return Results.BadRequest("Description is required.");
        }

        bool wasUpdated = await service.UpdateAsync(id, dto);

        return wasUpdated
            ? Results.NoContent()
            : Results.NotFound();
    });


    // DELETE handles removal. Results.NoContent means the deletion worked and there is no response body.
    group.MapDelete("/{id:int}", async (int id, ILearningTopicService service) =>
    {
        bool wasDeleted = await service.DeleteAsync(id);

        return wasDeleted
            ? Results.NoContent()
            : Results.NotFound();
    });
}
static void MapStudySessionEndpoints(WebApplication app, string routePrefix)
{
    RouteGroupBuilder group = app.MapGroup(routePrefix);
    group.MapPost("", async (
    CreateStudySessionDto dto,
    IStudySessionsService service) =>
{
    try
    {
        StudySessionDto createdSession = await service.Create(dto);
        return Results.Created(
        $"/api/study-sessions/{createdSession.Id}", createdSession
        );
    }
    catch (KeyNotFoundException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch (ArgumentOutOfRangeException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});
    group.MapGet("", async (IStudySessionsService service) =>
    {
        List<StudySessionDto> sessions = await service.GetAllAsync();
        return Results.Ok(sessions);
    });
    group.MapGet("/{id:int}", async (
      int id,
      IStudySessionsService service) =>
  {
      StudySessionDto session = await service.GetById(id);
      return Results.Ok(session);
  });
    group.MapDelete("/{id:int}", async (
     int id,
     IStudySessionsService service) =>
 {
     await service.DeleteAsync(id);
     return Results.NoContent();
 });
}
