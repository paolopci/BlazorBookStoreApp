# Repository Guidelines

## Codex Interaction
- Rispondi sempre in italiano, indipendentemente dalla lingua del prompt, salvo quando il contenuto richiesto deve rimanere in un'altra lingua.

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

## Mapping & DTO Guidelines
- Use AutoMapper 15.x with profiles stored under `BookStoreApp.API/Configurations` (e.g., `MapperConfig`) and register them via `builder.Services.AddAutoMapper(typeof(MapperConfig));`.
- Create dedicated DTOs for each controller scenario: `<Feature>ReadDto` for responses, `<Feature>CreateDto` for POST payloads, `<Feature>UpdateDto` for PUT/PATCH; never expose EF entities directly over the wire.
- Prefer calculated properties (e.g., `FullName` combining `FirstName` + `LastName`) in DTOs when you need derived data; configure mappings in `MapperConfig` using `CreateMap<Source, Destination>()` and `ForMember` when necessary.
- Keep DTOs lean by excluding database-only columns (audit fields, navigation collections) to prevent overposting and reduce payload size.
- Quando esponi relazioni annidate (es. autore con elenco libri) usa DTO dedicati per le collezioni figlie che non referenziano il DTO padre, così da evitare cicli JSON e ridurre payload ricorsivi.

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

## Database Schema Notes
- Current SQL schema exposes two tables: `Authors` (Id identity, FirstName/LastName NVARCHAR(50), Bio NVARCHAR(250)) and `Books` (Id identity, Title NVARCHAR(50), Year INT, ISBN NVARCHAR(50) unique, Summary NVARCHAR(250), Image NVARCHAR(50), Price DECIMAL(18,2), AuthorId FK).
- Keep the EF Core model aligned by capping string lengths to the same sizes and using `decimal(18, 2)` for money values; ensure `Books.AuthorId` enforces the foreign key constraint `FK_Books_ToTable`.
- Navigation properties should be initialized (e.g., `Books = new HashSet<Book>()`) to avoid `NullReferenceException` when adding related entities manually.

## EF Core Reverse Engineering
- When tables already exist, regenerate the context and entities with `dotnet ef dbcontext scaffold "<connection-string>" Microsoft.EntityFrameworkCore.SqlServer --context BookStoreDbContext --context-dir Data --output-dir Data --namespace BookStoreApp.API.Data --use-database-names`.
- In PowerShell you may split the command across multiple lines using the backtick `` ` `` for readability; on a single line remove the continuation characters.
- Confirm the project references `Microsoft.EntityFrameworkCore.SqlServer` and `Microsoft.EntityFrameworkCore.Tools` before running the scaffold command.
