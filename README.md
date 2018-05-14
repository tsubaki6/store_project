# Store .Net Project

## Running the project

After cloning or downloading the project you should be able to run it using an In Memory database immediately.

If you wish to use the sample with a persistent database, you will need to run its Entity Framework Core migrations before you will be able to run the app, and update `ConfigureServices` method in `Startup.cs`.

### Configuring the sample to use SQL Server

1. Update `Startup.cs`'s `ConfigureDevelopmentServices` method as follows:

```
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            //ConfigureTestingServices(services);

            // use real database
            ConfigureProductionServices(services);

        }
```

1. Ensure your connection strings in `appsettings.json` point to a local SQL Server instance.

2. Open a command prompt in the Web folder and execute the following commands:

```
dotnet restore
dotnet ef database update -c catalogcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
dotnet ef database update -c appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj
```

These commands will create two separate databases, one for the store's catalog data and shopping cart information, and one for the app's user credentials and identity data.

3. Run the application.
The first time you run the application, it will seed both databases with data such that you should see products in the store, and you should be able to log in using the demouser@microsoft.com account.

Note: If you need to create migrations, you can use these commands:
```
-- create migration (from Web folder CLI)
dotnet ef migrations add InitialModel --context catalogcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Data/Migrations

dotnet ef migrations add InitialIdentityModel --context appidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Identity/Migrations
```
