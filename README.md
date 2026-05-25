# Study Tracker API

Study Tracker API is a backend portfolio project built with ASP.NET Core Web API using the minimal API style.

The goal of the project is to track learning topics and their progress status, with a simple structure that can later grow into a more complete backend application.

## Current State

At the current stage, the project already has:

- Learning topics CRUD
- In-memory storage
- A separate service layer
- Request DTOs in separate files
- Basic validation for `POST` and `PUT`

## Current Features

- `GET /api/topics` — get all topics
- `GET /api/topics/{id}` — get topic by id
- `POST /api/topics` — create a topic
- `PUT /api/topics/{id}` — update a topic
- `DELETE /api/topics/{id}` — delete a topic
- In-memory storage for learning topics
- Validation for request data in `POST` and `PUT`

HTTP results currently used:

- `200 OK`
- `201 Created`
- `204 No Content`
- `400 Bad Request`
- `404 Not Found`

## Architecture

- `Program.cs` maps endpoints, validates incoming requests, resolves `LearningTopicService` from DI, and returns HTTP results.
- `LearningTopicService` contains CRUD logic and stores topics in memory.
- `DTOs` contain request models used for `POST` and `PUT` bodies.
- `Models` contain the main `LearningTopic` record.

Dependency injection is configured with:

```csharp
builder.Services.AddSingleton<LearningTopicService>();
```

## Data Model

Current model:

```csharp
public record LearningTopic(int Id, string Title, string Status);
```

Example topic:

```json
{
  "id": 1,
  "title": "ASP.NET Core Web API",
  "status": "Learning"
}
```

Allowed statuses:

- `Planned`
- `Learning`
- `Partial`
- `Confirmed`

Validation rules:

- `Title` cannot be empty
- `Status` must be one of the allowed values above

## Run Locally

```bash
cd StudyTracker.Api
dotnet run
```

By default, the API runs locally on:

```text
http://localhost:5007
```

## Storage

The project currently uses in-memory storage. Data exists only while the application is running, and resets after restart.

## Planned Improvements

- PostgreSQL
- Entity Framework Core
- Authentication and roles
- Unit tests
- Integration tests
- Docker
- Evidence system
