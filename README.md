# Study Tracker API

Study Tracker API is a backend portfolio project built with ASP.NET Core Web API.

The main goal of this project is to track learning topics and connect them with real proof of progress.

Core idea:

> A plan is not knowledge.  
> A topic should be confirmed only when there is evidence.

At the current stage, the project implements basic in-memory CRUD operations for learning topics.

---

## Tech Stack

Current stack:

- C#
- ASP.NET Core Web API
- REST API
- In-memory data storage

Planned stack:

- PostgreSQL
- Entity Framework Core
- Validation
- JWT Authentication
- Unit tests
- Integration tests
- Docker

---

## Current Features

The first version supports basic CRUD operations for learning topics:

- Get all learning topics
- Get one learning topic by id
- Create a new learning topic
- Update an existing learning topic
- Delete a learning topic

The API also uses proper HTTP status codes:

- `200 OK` — successful request
- `201 Created` — new topic was created
- `204 No Content` — topic was deleted successfully
- `404 Not Found` — topic with the requested id was not found

---

## Data Model

Current temporary model:

```csharp
record LearningTopic(int Id, string Title, string Status);

Example topic:

{
  "id": 1,
  "title": "ASP.NET Core Web API",
  "status": "Learning"
}

Current statuses used in examples:

Planned
Learning

Later, the project will support a more complete status system:

Planned
Learning
Partial
Confirmed
API Endpoints
Get all topics
GET /api/topics

Returns all learning topics.

Example:

curl -i http://localhost:5007/api/topics
Get topic by id
GET /api/topics/{id}

Returns one topic by id.

Example:

curl -i http://localhost:5007/api/topics/2

If the topic does not exist, the API returns:

404 Not Found
Create topic
POST /api/topics

Creates a new learning topic.

Request body:

{
  "title": "Validation",
  "status": "Planned"
}

Example:

curl -i -X POST http://localhost:5007/api/topics \
  -H "Content-Type: application/json" \
  -d '{"title":"Validation","status":"Planned"}'

Expected result:

201 Created

Example response:

{
  "id": 4,
  "title": "Validation",
  "status": "Planned"
}
Update topic
PUT /api/topics/{id}

Updates an existing learning topic by id.

Request body:

{
  "title": "EF Core",
  "status": "Learning"
}

Example:

curl -i -X PUT http://localhost:5007/api/topics/2 \
  -H "Content-Type: application/json" \
  -d '{"title":"EF Core","status":"Learning"}'

If the topic exists, the API returns:

200 OK

If the topic does not exist, the API returns:

404 Not Found
Delete topic
DELETE /api/topics/{id}

Deletes a learning topic by id.

Example:

curl -i -X DELETE http://localhost:5007/api/topics/2

If the topic exists, the API returns:

204 No Content

If the topic does not exist, the API returns:

404 Not Found
Run Locally

Go to the project folder:

cd StudyTracker.Api

Run the API:

dotnet run

The API runs locally on:

http://localhost:5007
Current Storage

This version uses an in-memory list:

List<LearningTopic> topics = new List<LearningTopic>();

That means the data exists only while the server is running.

If the server is stopped and started again, all created, updated, and deleted topics are reset to the initial hardcoded list.

Persistent storage will be added later with PostgreSQL and Entity Framework Core.

What I Practiced

In this version, I practiced:

creating an ASP.NET Core Web API project
running a local backend server
working with localhost and ports
creating REST endpoints
using GET, POST, PUT, and DELETE
working with route parameters
reading JSON request bodies
using DTO records for requests
returning correct HTTP status codes
testing endpoints with curl
using Git and pushing the project to GitHub
Next Steps

Planned improvements:

Move models and DTOs into separate files
Add folder structure
Add validation for request data
Add PostgreSQL
Add Entity Framework Core
Add migrations
Add evidence system
Add authentication and roles
Add unit and integration tests
Add Docker support
```
