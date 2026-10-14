# GIBDD

A historical educational traffic-records project with a WPF client, an ASP.NET Core / Angular web client, and SQL Server persistence. **Setup is required.** The repository contains several unfinished variants; a full solution build is not currently a verified launch path.

## Project map

| Project | Framework | Role |
| --- | --- | --- |
| `PL/PL_WPF.csproj` | .NET Framework 4.7.2 | Windows WPF client |
| `GIBDD/DAL.csproj` | .NET Framework 4.7.2, EF6 | Original desktop data layer |
| `BLL/BLL.csproj` | .NET Framework 4.7.2 | Desktop business layer; see the reference blocker below |
| `PL_ASP_5/PL_ASP_5.csproj` | .NET 5 | Web host; Angular sources are in `PL_ASP_5/ClientApp` |
| `BLL_ASP/BLL_ASP.csproj`, `DAL_ASP_5/DAL_ASP_5.csproj` | .NET 5, EF Core 5 | Web business/data layers |

## Prerequisites

- Windows and Visual Studio with .NET desktop development and the .NET Framework 4.7.2 targeting pack for the WPF variant. It cannot run as a WPF app on macOS.
- The historical .NET 5 SDK/runtime and ASP.NET workload for the web variant, plus SQL Server Express/LocalDB or another local SQL Server instance.
- Node.js/npm compatible with the Angular 8.2 app in `PL_ASP_5/ClientApp/package.json`.
- Restored NuGet packages and a new local database. Connection strings in this teaching repository are examples tied to the original machine, not access to a hosted database.

## Web variant: setup steps

1. Open `PL_ASP_5/PL_ASP_5.csproj` directly to avoid the missing project in the root solution. Restore its NuGet dependencies.
2. In `PL_ASP_5/appsettings.json`, replace `ConnectionStrings:DevConnection` with your local SQL Server connection. For a Windows LocalDB example:

   ```json
   {
     "ConnectionStrings": {
       "DevConnection": "Server=(localdb)\\MSSQLLocalDB;Database=GibddDB2;Trusted_Connection=True;MultipleActiveResultSets=True;"
     }
   }
   ```

   Merge this section into the existing file; retain its logging settings. The double backslash above is JSON escaping. The example uses Windows authentication and contains no password. Replace the server/database for your environment.
3. Create the database schema **before starting the web host**. `Startup.CreateUserRoles` queries Identity tables during startup. In Visual Studio's Package Manager Console, select `DAL_ASP_5` as the default project, use `PL_ASP_5` as startup project, and run EF Core `Update-Database`. The CLI alternative, with a compatible EF Core 5 tool installed, is:

   ```powershell
   cd PL_ASP_5
   dotnet ef database update --project ../DAL_ASP_5 --startup-project .
   ```

   Run it from `PL_ASP_5`: `DesignTimeDbContextFactory` reads `appsettings.json` from the current directory. The historical UTF-16 export `БД.sql` is an alternative to investigate, not an additional migration: its `FILENAME` paths must be changed to paths on your machine before use.
4. Install the client dependencies from `PL_ASP_5/ClientApp` with a compatible npm, then return to `PL_ASP_5`:

   ```powershell
   cd ClientApp
   npm install
   cd ..
   dotnet run --project PL_ASP_5.csproj
   ```

   The Development host starts Angular through its `npm start` script. The checked-in launch profile uses `https://localhost:5001` and `http://localhost:5000`. Configure/trust your local ASP.NET development certificate if using HTTPS.
5. Verify account/role setup in `Startup.CreateUserRoles` and optional sample data in `DAL_ASP_5/ContextInizializer.cs`. The sample-data call is commented out. Existing seeded accounts are demonstration data, not credentials for an external service.

## Desktop variant: remaining work

1. Resolve `BLL/BLL.csproj` referencing the .NET 5 `DAL_ASP_5` project from a .NET Framework 4.7.2 project. This is an incompatible project reference. Decide whether to restore the desktop EF6 layer (`GIBDD/DAL.csproj`) and adapt its consumers, or migrate the desktop stack together. Merely changing a connection string does not fix this reference.
2. Set the local SQL Server connection named `GIBDDContext` in `PL/App.config` (the executable's configuration). `GIBDD/App.config` also has an original-machine example. The checked-in database names differ between these files; choose the database for the variant you are actually running.
3. Restore NuGet packages, prepare the EF6 schema for the desktop model, select `PL_WPF` as startup project, and build/run on Windows after resolving the reference blocker. Do not assume the web database/migrations are interchangeable with the desktop variant.

## Known placeholders and blockers

- `GIBDD.sln` still references `PL_Angular/PL_Angular.csproj`, which is absent and ignored. The tracked Angular client is instead under `PL_ASP_5/ClientApp`. Open the desired project directly; repair the solution's historical project list before using it as a full-build entry point.
- The desktop framework-reference mismatch described above remains unresolved.
- `ClientApp/package.json` mixes Angular 8 with `@angular/material-moment-adapter` 11. Dependency versions may need alignment before the frontend builds on a fresh environment.
- The web project contains duplicate EF Core Tools package references with different versions. Align them with the EF Core runtime/tooling if restore or migrations report a conflict.
- `.vs/` is generated local Visual Studio/IIS configuration. Visual Studio recreates it; restoring it from Git is not a launch step.

This setup documents the remaining work. A Windows build, SQL Server migration, login flow, and Angular UI have not been verified. See the [EF Core Package Manager Console reference](https://learn.microsoft.com/en-us/ef/core/cli/powershell) for migration commands.
