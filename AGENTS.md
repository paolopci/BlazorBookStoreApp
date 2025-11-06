# Repository Guidelines

## Project Structure & Module Organization
- Solution root `BookStoreApp.sln` references the API project under `BookStoreApp.API/`.
- `BookStoreApp.API/Controllers/` holds ASP.NET Core controllers (e.g., `WeatherForecastController`); add new endpoints here.
- Shared configuration lives in `BookStoreApp.API/appsettings*.json`; use user secrets for sensitive overrides.
- Build artifacts flow into `BookStoreApp.API/bin/` and `BookStoreApp.API/obj/`; keep generated files out of commits.

## Build, Test, and Development Commands
- `dotnet restore` ensures all NuGet dependencies are available.
- `dotnet build BookStoreApp.sln` compiles the full solution and surfaces compile-time issues.
- `dotnet run --project BookStoreApp.API` launches the API locally with Swagger at `/swagger`.
- `dotnet watch run --project BookStoreApp.API` enables hot-reload for iterative work on endpoints and models.

## Coding Style & Naming Conventions
- Follow standard .NET guidelines: 4-space indentation, PascalCase for classes/methods, camelCase for locals/parameters.
- Group usings with `System.*` first, then third-party, then internal namespaces; remove unused usings before committing.
- Name controllers as `<Feature>Controller`, DTOs as `<Feature>Dto`, and services as `<Feature>Service`.
- Run `dotnet format BookStoreApp.sln` before submission to enforce consistent styling.

## Testing Guidelines
- Add automated tests in a sibling project such as `BookStoreApp.Tests/` using xUnit (`dotnet new xunit -o BookStoreApp.Tests`).
- Test files should mirror feature names (`WeatherForecastTests.cs`) and use the `[Fact]`/`[Theory]` pattern.
- Execute `dotnet test BookStoreApp.sln` locally; aim to keep code paths covered, especially for controller actions and services.
- When introducing new API endpoints, include integration or unit tests that validate both success and failure scenarios.

## Commit & Pull Request Guidelines
- Commit messages use the imperative mood (`Add books endpoint`) and should group related changes; avoid bundling unrelated refactors.
- Reference work items in the body (`Refs #123`) and summarize user-visible impact when applicable.
- Pull requests must include a short summary, screenshots for UI-consuming changes (if applicable), and explicit test evidence (`dotnet test` output).
- Request review from at least one teammate familiar with the touched area and resolve all comments before merge.

## Environment & Configuration Tips
- Keep environment-specific secrets out of source control; prefer `dotnet user-secrets` or environment variables.
- Update `appsettings.Development.json` for local overrides but document any required keys in the PR description.
