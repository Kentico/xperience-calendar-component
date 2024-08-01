# Contributing Setup

## Required Software

The requirements to setup, develop, and build this project are listed below.

### .NET Runtime

.NET SDK 7.0 or newer

- <https://dotnet.microsoft.com/en-us/download/dotnet/8.0>
- See `global.json` file for specific SDK requirements.

### Node.js Runtime

- [Node.js](https://nodejs.org/en/download) 20.11.0 or newer.
- [NVM for Windows](https://github.com/coreybutler/nvm-windows) to manage multiple installed versions of Node.js.
- See `engines` in the solution `package.json` for specific version requirements.

## Development environment setup

1. Download/clone this repository.
2. Run `npm run build` in the `/src/Kentico.Xperience.CalendarComponent/Client/`.
3. Create an instance of [Kentico Xperience 13 administration](https://docs.kentico.com/13/installation/installing-xperience).
4. [Create a database](https://docs.kentico.com/13/installation/additional-database-installation).
5. Start the DancingGoatCore site.
6. Open `WebApp.sln` of your administration project.
7. Start the *CMSApp* project in IIS Express.
8. Go to the *Forms* application.
9. Edit a form.
10. Open Form builder and add the Calendar form component.
11. Configure the properties of the component.

## Development Workflow

1. Create a new branch with one of the following prefixes.

   - `feat/` - for new functionality.
   - `refactor/` - for restructuring of existing features.
   - `fix/` - for bugfixes.

2. Run `dotnet format` against the `Kentico.Xperience.RepoTemplate` solution.

   > use `dotnet: format` VS Code task.

3. Commit changes, with a commit message preferably following the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/#summary) convention.

4. Once ready, create a PR on GitHub. The PR will need to have all comments resolved and all tests passing before it will be merged.

   - The PR should have a helpful description of the scope of changes being contributed.
   - Include screenshots or video to reflect UX or UI updates.
   - Indicate if new settings need to be applied when the changes are merged - locally or in other environments.
