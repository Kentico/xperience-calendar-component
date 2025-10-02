# GitHub Copilot Instructions for Kentico Xperience Calendar Component

## Project Overview

This repository contains the Kentico Xperience 13 Calendar Form Component, a .NET library that provides calendar form component integration for Kentico Xperience 13. The component enables date selection with features like single date, date and time, date ranges, and multiple dates selection, with programmatic exclusion of specific dates and times.

## Technologies & Stack

- **Backend**: .NET (C#) targeting ASP.NET Core 6.0+
- **Frontend**: JavaScript (Webpack bundled), Flatpickr v4
- **CMS**: Kentico Xperience 13 (>= 13.0.152)
- **Build System**: .NET SDK 7.0+, Node.js 20.11.0+

## Repository Structure

- `/src/Kentico.Xperience.CalendarComponent/` - Main library source code
  - `Components/` - Calendar form components (CalendarFormComponent, MultiCalendarFormComponent)
  - `ValueProviders/` - Data provider interfaces and implementations
  - `Client/` - Frontend JavaScript/Webpack source
  - `wwwroot/` - Bundled static assets
- `/examples/DancingGoatCore/` - Example implementation using DancingGoat demo site
- `/tests/` - Unit and integration tests
- `/docs/` - User and developer documentation

## Code Style & Conventions

### C# Code
- Follow .NET coding conventions
- Use XML documentation comments for public APIs
- Run `dotnet format` before committing (enforced by CI)
- Namespace pattern: `Kentico.Xperience.CalendarComponent.*`
- Use dependency injection for services
- Follow existing patterns for form component implementations

### JavaScript Code
- Use modern ES6+ syntax where appropriate
- Bundle via Webpack (configuration in `/src/Kentico.Xperience.CalendarComponent/Client/webpack.config.js`)
- Output to `/src/Kentico.Xperience.CalendarComponent/wwwroot/js/`

### Comments
- Use `/* */` style for multi-line comments in C# when matching existing code style
- Avoid unnecessary comments; code should be self-documenting
- Document complex business logic and integration points

## Development Workflow

### Setup
1. Clone the repository
2. Run `npm run build` in `/src/Kentico.Xperience.CalendarComponent/Client/`
3. Restore dependencies: `dotnet restore`
4. Build solution: `dotnet build`

### Branch Naming
- `feat/` - New functionality
- `refactor/` - Code restructuring
- `fix/` - Bug fixes

### Before Committing
1. Run `dotnet format` against the solution
2. Ensure all tests pass: `dotnet test`
3. Build successfully: `dotnet build --configuration Release`
4. Follow [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/#summary) for commit messages

### Pull Requests
- Provide clear description of changes
- Include screenshots/video for UI changes
- Ensure CI checks pass (format check, build, tests)
- All comments must be resolved before merge

## Building & Testing

### Build
```bash
# Restore dependencies
dotnet restore --locked-mode

# Build solution
dotnet build --configuration Release --no-restore

# Build JavaScript assets
cd src/Kentico.Xperience.CalendarComponent/Client
npm run build
```

### Testing
```bash
# Run all tests
dotnet test --configuration Release --no-build --no-restore
```

### Formatting
```bash
# Check formatting (CI mode)
dotnet format Kentico.Xperience.CalendarComponent.sln --exclude ./examples/** --verify-no-changes

# Fix formatting
dotnet format Kentico.Xperience.CalendarComponent.sln --exclude ./examples/**
```

## Key Patterns & Concepts

### Form Components
- Inherit from Kentico's form component base classes
- Implement property classes for configuration
- Use configurators for complex property editors
- Register in `CalendarStartupExtensions`

### Data Providers
- Implement `ICalendarDataProvider` for custom date/time exclusion logic
- Register providers via dependency injection
- Use `CalendarProviderStorage` for provider management

### Integration Points
- Form Builder integration via component registration
- Dependency injection via `IServiceCollection` extensions
- JavaScript/C# bridge for dynamic behavior

## Important Files

- `src/Kentico.Xperience.CalendarComponent/CalendarStartupExtensions.cs` - Component registration and DI setup
- `Directory.Packages.props` - Centralized NuGet package version management
- `global.json` - .NET SDK version specification
- `.github/workflows/ci.yml` - CI/CD pipeline configuration

## Documentation

- README.md - Project overview and quick start
- `/docs/Contributing-Setup.md` - Development environment setup
- `/docs/Single-Value-Calendar-Component.md` - Single date picker documentation
- `/docs/Multi-Value-Calendar-Component.md` - Multiple date picker documentation
- `/docs/Usage-Guide.md` - General usage instructions

## Support & Contribution

- Full support with 7-day bug-fix policy
- Follow [Kentico's CONTRIBUTING.md](https://github.com/Kentico/.github/blob/main/CONTRIBUTING.md)
- Adhere to [Kentico's CODE_OF_CONDUCT](https://github.com/Kentico/.github/blob/main/CODE_OF_CONDUCT.md)
- Security issues: [SECURITY.md](https://github.com/Kentico/.github/blob/main/SECURITY.md)

## When Making Changes

1. **Understand the context**: Review related documentation and existing implementations
2. **Minimal changes**: Make the smallest change necessary to achieve the goal
3. **Test thoroughly**: Ensure changes work with Kentico Xperience 13
4. **Maintain compatibility**: Don't break existing public APIs
5. **Follow patterns**: Match existing code structure and conventions
6. **Document changes**: Update relevant documentation in `/docs/` if needed
