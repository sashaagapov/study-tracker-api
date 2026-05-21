using StudyTracker.Api.Models;
using StudyTracker.Api.DTOs;
using StudyTracker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Register framework and application services in the dependency injection container.
builder.Services.AddOpenApi();
builder.Services.AddSingleton<LearningTopicService>();

var app = builder.Build();

// Enable OpenAPI only during development to keep the setup beginner-friendly.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirect HTTP requests to HTTPS when possible.
app.UseHttpsRedirection();

// Return the full list of learning topics.
app.MapGet("/api/topics", (LearningTopicService service) =>
{
    return Results.Ok(service.GetAll());
});

// Return a single topic by id or 404 when it does not exist.
app.MapGet("/api/topics/{id}", (int id, LearningTopicService service) =>
{
    LearningTopic? topic = service.GetById(id);

    if (topic == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(topic);
});

// Create a new topic from the request body and return 201 Created.
app.MapPost("/api/topics", (CreateLearningTopicRequest request, LearningTopicService service) =>
{
    if (string.IsNullOrWhiteSpace(request.Title))
    {
        return Results.BadRequest("Title is required.");
    }
    if (request.Status != "Planned" &&
    request.Status != "Learning" &&
    request.Status != "Partial" &&
    request.Status != "Confirmed")
    {
        return Results.BadRequest("Invalid status.");
    }
    LearningTopic newTopic = service.Create(request);

    return Results.Created($"/api/topics/{newTopic.Id}", newTopic);
});

// Update an existing topic or return 404 when the topic is missing.
app.MapPut("/api/topics/{id}", (int id, UpdateLearningTopicRequest request, LearningTopicService service) =>
{
    LearningTopic? updatedTopic = service.Update(id, request);

    if (updatedTopic == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(updatedTopic);
});

// Delete an existing topic or return 404 when the topic is missing.
app.MapDelete("/api/topics/{id}", (int id, LearningTopicService service) =>
{
    bool wasDeleted = service.Delete(id);

    if (!wasDeleted)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});

// Start the web application.
app.Run();
