# Study Tracker API

Study Tracker API is a learning project for practicing intern-level ASP.NET Core Web API concepts.

Phase 1 keeps the existing Minimal API style and refactors the backend from in-memory storage to EF Core with SQL Server.

## What Was Preserved

- The existing ASP.NET Core Minimal API project.
- The original learning-topic CRUD idea.
- The simple `Controller/Endpoint -> Service -> Data` flow.
- Legacy `/api/topics` routes, so old HTTP requests still work.

## What Was Refactored

- `LearningTopicService` now uses EF Core instead of an in-memory `List<LearningTopic>`.
- PostgreSQL/Npgsql was replaced with SQL Server.
- The service is registered through `ILearningTopicService`.
- `LearningTopic` became a fuller EF Core entity.
- DTOs now separate API request/response shapes from database entities.

## What This Practices

- Entity
- DTO
- DbContext
- DbSet
- EF Core relationship mapping
- SQL Server provider setup
- Dependency Injection
- async/await
- IQueryable vs ToListAsync
- REST CRUD
- Practical SOLID basics

## Run Backend

From this folder:

```bash
dotnet restore
dotnet run
```

Default local HTTP URL from `StudyTracker.Api.http`:

```text
http://localhost:5007
```

## SQL Server Connection

`appsettings.json` contains this example connection string:

```json
"DefaultConnection": "Server=localhost,1433;Database=StudyTrackerDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
```

You need SQL Server running before EF Core can create or query the real database.

## EF Core Migrations

Install the EF tool if needed:

```bash
dotnet tool install --global dotnet-ef
```

Create a migration:

```bash
dotnet ef migrations add InitialCreate
```

Apply it to SQL Server:

```bash
dotnet ef database update
```

## Main Endpoints

- `GET /api/learning-topics`
- `GET /api/learning-topics?status=InProgress&difficulty=Medium&search=api`
- `GET /api/learning-topics/{id}`
- `POST /api/learning-topics`
- `PUT /api/learning-topics/{id}`
- `DELETE /api/learning-topics/{id}`

Legacy aliases still exist:

- `GET /api/topics`
- `GET /api/topics/{id}`
- `POST /api/topics`
- `PUT /api/topics/{id}`
- `DELETE /api/topics/{id}`

## Learning Checklist

- Read `Program.cs` to understand Minimal API endpoints and DI.
- Read `Services/ILearningTopicService.cs` to understand the service contract.
- Read `Services/LearningTopicService.cs` to understand async EF Core queries.
- Read `Data/AppDbContext.cs` to understand DbSet, relationships, and seed data.
- Read `Models/LearningTopic.cs` and `Models/StudySession.cs` to understand entities.
- Read `DTOs/` to understand API data shapes.
- Read `Notes/BackendBasics.md` for short interview explanations.
