var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
List<LearningTopic> topics = new List<LearningTopic>
    {
        new LearningTopic(1, "ASP.NET Core Web API", "Learning"),
        new LearningTopic(2, "EF Core", "Planned"),
        new LearningTopic(3, "PostgreSQL", "Planned")
    };

app.UseHttpsRedirection();

app.MapGet("/api/topics", () =>
{
    return Results.Ok(topics);
});

app.MapGet("/api/topics/{id}", (int id) =>
{
    LearningTopic? foundTopic = null;
    foreach (LearningTopic topic in topics)
    {
        if (topic.Id == id)
        {
            foundTopic = topic;
        }
    }
    if (foundTopic == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(foundTopic);
});

app.MapPost("/api/topics", (CreateLearningTopicRequest request) =>
{
    int newId = topics.Count + 1;
    LearningTopic newTopic = new LearningTopic(newId, request.Title, request.Status);
    topics.Add(newTopic);
    return Results.Created($"/api/topics/{newTopic.Id}", newTopic);
});
app.MapPut("/api/topics/{id}", (int id, UpdateLearningTopicRequest request) =>
{
    int foundIndex = -1;

    for (int i = 0; i < topics.Count; i++)
    {
        if (topics[i].Id == id)
        {
            foundIndex = i;
        }
    }

    if (foundIndex == -1)
    {
        return Results.NotFound();
    }

    LearningTopic updatedTopic = new LearningTopic(id, request.Title, request.Status);

    topics[foundIndex] = updatedTopic;

    return Results.Ok(updatedTopic);
});
app.MapDelete("/api/topics/{id}", (int id) =>
{
    int foundIndex = -1;

    for (int i = 0; i < topics.Count; i++)
    {
        if (topics[i].Id == id)
        {
            foundIndex = i;
        }
    }

    if (foundIndex == -1)
    {
        return Results.NotFound();
    }

    topics.RemoveAt(foundIndex);

    return Results.NoContent();
});

app.Run();

record LearningTopic(int Id, string Title, string Status);
record CreateLearningTopicRequest(string Title, string Status);
record UpdateLearningTopicRequest(string Title, string Status);
