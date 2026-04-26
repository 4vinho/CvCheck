# Repository Guidelines

## Project Structure & Architecture

This backend lives in `src/backend/` and follows a layered Clean Architecture style inside a single ASP.NET Core project:

- `api/App/`: API boundary. Keep controllers, HTTP contracts, and request/response DTOs here.
- `api/Core/`: business center. Keep entities, interfaces, enums, and result models here. `Core` must not depend on `App` or external providers.
- `api/Infra/`: technical implementations. This layer contains EF Core, ASP.NET Identity, dependency injection, migrations, and external integrations.
- `api/Program.cs`: composition root only. Wire services, middleware, Swagger, and startup behavior here.
- `tests/backend/`: xUnit test project for API and unit tests.

Prefer feature folders inside each layer, for example `Auth/`, `Accounts/`, or `CvAnalysis/`.

## Database & Infrastructure Flow

The API uses PostgreSQL through EF Core and `ASP.NET Core Identity`.

- Connection string key: `ConnectionStrings:DefaultConnection`
- EF registration: `api/Infra/DependencyInjection/ServiceCollectionExtensions.cs`
- DbContext: `api/Infra/Data/ApplicationDbContext.cs`
- Migrations: `api/Infra/Data/Migrations/`

At startup, `Program.cs` calls `AddInfrastructure(builder.Configuration)` and then `MigrateDatabaseAsync()`. In development, the app applies pending migrations automatically. Local database infra lives in `src/infra/`; copy `.env.example` to `.env` and run `docker compose up -d --build`.

## Build, Test, and Development Commands

Run from `src/backend/`.

- `dotnet restore api/api.csproj`: restore packages.
- `dotnet build api/api.csproj`: compile the backend.
- `dotnet run --project api/api.csproj --launch-profile http`: run the API on `http://localhost:5038`.
- `dotnet watch --project api/api.csproj run`: run with hot reload.
- `dotnet test tests/backend/backend.Tests.csproj`: run the automated tests.

Swagger UI is available in development at `/swagger`.

## Coding Style & Naming Conventions

Use 4-space indentation, file-scoped namespaces, nullable reference types, and one public type per file.

- `PascalCase`: classes, records, methods, properties.
- `camelCase`: locals and parameters.
- Controllers end with `Controller`.
- DTOs follow `SomethingRequest` and `SomethingResponse`.
- Interfaces in `Core` start with `I`.

Keep business rules out of controllers and infrastructure details out of `Core`.

## Testing, Commits, and Safety

Use xUnit. Name files `SubjectTests.cs` and methods `Action_WhenCondition_Result`. Favor contract, validation, persistence, and auth behavior coverage.

Follow Conventional Commits, for example `feat(auth): cria cadastro local` or `chore(git): ignora cache dotnet`. Separate commits by intent.

Do not commit secrets. Keep real credentials out of `appsettings*.json`. Ignore generated outputs such as `bin/`, `obj/`, and `src/backend/.dotnet/`.
