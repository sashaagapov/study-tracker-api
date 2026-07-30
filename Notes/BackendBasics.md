# Backend Basics

This file explains the Phase 1 backend in interview-friendly language.

## Endpoint

An endpoint handles HTTP input and output. In this project, endpoints are in `Program.cs`.

Example flow:

`GET /api/learning-topics` -> endpoint receives query parameters -> service is called -> endpoint returns `Results.Ok(...)`.

Endpoints should not contain database logic. They should read the request, call the correct service method, and return the correct HTTP result.

## Service

A service contains application/business logic. In this project, `LearningTopicService` knows how to create, update, delete, and search learning topics.

The endpoint depends on `ILearningTopicService`, not directly on `LearningTopicService`. This is a practical example of Dependency Inversion.

## DbContext

`AppDbContext` represents a session with the database. EF Core uses it to build queries, track entity changes, and save changes.

In this project:

```csharp
public class AppDbContext : DbContext
```

The service receives `AppDbContext` through dependency injection.

## DbSet

`DbSet<T>` is the EF Core entry point for a table-like collection.

In this project:

```csharp
public DbSet<LearningTopic> LearningTopics { get; set; }
public DbSet<StudySession> StudySessions { get; set; }
```

You can query a `DbSet`, add entities to it, remove entities from it, and save changes through the context.

## Entity

An entity represents the shape EF Core stores in the database.

Examples:

- `LearningTopic`
- `StudySession`

Entities can have relationships. `LearningTopic` has many `StudySessions`.

## DTO

A DTO is a data shape used by the API. It is different from the entity so the API does not expose the database model directly.

Examples:

- `CreateLearningTopicDto` is used for `POST`.
- `UpdateLearningTopicDto` is used for `PUT`.
- `LearningTopicDto` is returned from the API.

## IEnumerable

`IEnumerable<T>` means "I can loop over these items." It is common after data is already in memory.

Example:

```csharp
IEnumerable<LearningTopicDto> topics = topicDtos;
```

## IQueryable

`IQueryable<T>` means "I am building a query." With EF Core, the SQL query has not been sent to the database yet.

In `LearningTopicService.GetAllAsync`, the query starts as:

```csharp
IQueryable<LearningTopic> query = _context.LearningTopics;
```

Filters are added before `ToListAsync`, so SQL Server receives one useful query instead of loading everything first.

## ICollection

`ICollection<T>` is useful for navigation properties because EF Core needs a collection it can add to and track.

In this project:

```csharp
public ICollection<StudySession> StudySessions { get; set; }
```

That means one `LearningTopic` can have many `StudySession` rows.

## ToListAsync

`ToListAsync` executes the EF Core query and returns the results as a `List<T>`.

Before `ToListAsync`: query is being built.

After `ToListAsync`: data is loaded into memory.

## async/await in Web API

Database calls can take time. `async/await` lets the server wait for the database without blocking a thread.

In this project:

```csharp
List<LearningTopic> topics = await query.ToListAsync();
```

That is why endpoint handlers and service methods return `Task`.
