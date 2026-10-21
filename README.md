# Study Tracker API

An ASP.NET Core Minimal API for organizing learning topics and recording study sessions. It uses EF Core with SQL Server. The database migration seeds three topics, including **ASP.NET Core Web API** and **EF Core**; those records are defined in [AppDbContext](Data/AppDbContext.cs), so you can inspect the example without running the service.

## Explore the project

| Area | What is here | Start with |
| --- | --- | --- |
| Learning topics | Create, list, filter, update, and delete topics; legacy `/api/topics` aliases | [Endpoint definitions](Program.cs) · [Topic service](Services/LearningTopicService.cs) · [HTTP requests](StudyTracker.Api.http) |
| Study sessions | Create, list, fetch, and delete sessions linked to topics | [Endpoint definitions](Program.cs) · [Session service](Services/StudySessionService.cs) |
| Persistence | SQL Server context, topic/session relationship, seeded topics, initial migration | [DbContext](Data/AppDbContext.cs) · [Migration](Migrations/20260727173710_InitialCreate.cs) |
| API shapes | Request and response records, difficulty and status enums | [DTOs](DTOs/) · [Models](Models/) |
| Learning notes | Short explanations of the backend concepts used here | [Backend basics](Notes/BackendBasics.md) |

### Example from the checked-in seed data

After applying the migration, `GET /api/learning-topics/1` can return the seeded **ASP.NET Core Web API** topic. This is an illustrative response shape derived from the seed and DTO, not a captured run; counts and dates can change after writes.

```json
{
  "id": 1,
  "title": "ASP.NET Core Web API",
  "description": "Practice endpoints, HTTP results, and service calls.",
  "difficulty": "Medium",
  "status": "InProgress",
  "createdAt": "2026-07-21T00:00:00Z",
  "updatedAt": null,
  "studySessionsCount": 0
}
```

The checked-in [HTTP request collection](StudyTracker.Api.http) covers topic CRUD, filtering, a legacy route, and an invalid-title example. To add a session for the seeded topic, send `POST /api/study-sessions` with a past date:

```json
{
  "learningTopicId": 1,
  "date": "2026-07-21T12:00:00Z",
  "durationMinutes": 45,
  "notes": "Practiced Minimal API routes."
}
```

This is a request example, not a captured result. Its fields come from [CreateStudySessionDto](DTOs/CreateStudySessionDto.cs); session routes are in [Program.cs](Program.cs).

## Run locally

Requires the .NET 10 SDK and a reachable SQL Server instance. Set `ConnectionStrings:DefaultConnection` for your own server through .NET configuration or edit the example in [appsettings.json](appsettings.json). Apply the existing migration before sending requests:

```bash
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet run --launch-profile http
```

If `dotnet-ef` is already installed, skip the install command. The HTTP launch profile uses `http://localhost:5007`; browse `/swagger` in Development or send requests from [StudyTracker.Api.http](StudyTracker.Api.http). `dotnet build` checks compilation. The repository does not contain an automated test project.

Topic `POST` requires a nonempty title and description; the `difficulty` value is `Easy`, `Medium`, or `Hard`. Session `POST` requires an existing topic ID, a date that is not in the future, and a positive duration. The source routes and service above are the authority for behavior and error handling.
