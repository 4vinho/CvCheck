# Repository Guidelines

## Project Structure & Module Organization

This backend lives in `src/backend/`. The API project is `api/`, with code organized by layer:

- `api/App/`: HTTP-facing code such as controllers and request/response DTOs.
- `api/Core/`: domain entities, interfaces, enums, and core results.
- `api/Infra/`: persistence, Identity integration, dependency injection, and external services.
- `api/Program.cs`: application bootstrap only; keep it lean.
- `tests/backend/`: xUnit test project with API and unit coverage.

Prefer feature-oriented subfolders inside each layer, for example `Auth/`, `Users/`, or `CvAnalysis/`.

## Build, Test, and Development Commands

Run commands from `src/backend/`.

- `dotnet restore api/api.csproj`: restores NuGet packages.
- `dotnet build api/api.csproj`: compiles the backend and catches integration errors early.
- `dotnet run --project api/api.csproj`: starts the API locally.
- `dotnet watch --project api/api.csproj run`: runs with hot reload.
- `dotnet test tests/backend/backend.Tests.csproj`: executes the backend test suite.

## Coding Style & Naming Conventions

Use standard C# conventions with 4-space indentation, file-scoped namespaces, and nullable reference types enabled. Keep one public type per file.

- `PascalCase`: classes, records, enums, methods, properties.
- `camelCase`: locals and parameters.
- Controllers end with `Controller`.
- DTOs use names like `RegisterRequest` and `RegisterResponse`.
- Interfaces in `Core` start with `I`, for example `IRegistrationService`.

## Testing Guidelines

Use xUnit for automated tests. Keep API/integration tests near the HTTP flow and unit tests near business or mapping rules.

- Test file naming: `SubjectTests.cs`
- Test method naming: `Action_WhenCondition_Result`
- Prefer covering validation, contracts, persistence effects, and auth behavior over trivial models.

Run `dotnet test` before opening a PR.

## Commit & Pull Request Guidelines

Follow Conventional Commits with short titles under 50 characters, for example:

- `feat(auth): cria cadastro local`
- `test(auth): cobre cadastro local`

Separate commits by intent: feature, refactor, test, docs, or chore. Pull requests should explain the goal, summarize key changes, list validation performed, and link the related work item. Include request/response examples when an API contract changes.

## Security & Configuration Tips

Do not commit secrets. Keep sensitive settings out of `appsettings*.json`; prefer environment variables or secret storage. Ignore generated outputs such as `bin/`, `obj/`, and local `.dotnet/` artifacts.
