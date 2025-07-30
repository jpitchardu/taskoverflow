# TaskOverflow

## How to run?

Running `make start`should get everything going and the client should be accessible on port `3000`, the api will be accessible on port `5000`

If you run into any errors during build or rebuild `make reset` should clear out all the previous containers and get it going again.

`make test` to run both server and client tests.

**Disclaimer: The db is not persistent through restarts**

## Cases Covered

- [x] Get all tasks
- [x] Create a task with validation
- [x] Complete a task
- [x] Delete a task
- [x] Loading state

## Assumptions made:

### Business Logic
- Once a task is completed there is no need to uncomplete it, user can just create a new one.
- Tasks should be simple, so only a title is needed.
- No need for due dates for an MVP.
- Tasks do not need to be undeleted they can just be recreated.

### Technical Decisions
- User won't have over 1k tasks so no need for pagination.
- API response time is fast enough to not need optimistic updates.
- No need for offline support for a single user MVP.
- Index on `IsCompleted` field for future filtering.
- Index on `UpdatedAt` field for future ETL support.
- Unit tests on services and repositories but e2e on api endpoints.
- React query > nextjs ssr due to familiarity.


**The title was set as 20 chars length maximum to be able to showcase validation easily**

## Tech Stack
**Backend:** ASP.Net Core, PostgreSQL, Entity Framework Core, xUnit  
**Frontend:** Next.js, TypeScript, React Query, shadcn/ui, vitest  
**Infrastructure:** Docker Compose

## API Endpoints
- `GET /api/tasks` - Get all tasks
- `POST /api/tasks` - Create task (validates title required, max 20 chars)
- `PATCH /api/tasks/{id}` - Toggle completion status
- `DELETE /api/tasks/{id}` - Delete task

## If this was going to production

- Redis caching for session data
- Persistent postgresql cluster with a read_only instance and regular backups
- A migration infrastructure for EF -> DB
- Authentication
- Tags for tasks and filtering
- Rate limiting.