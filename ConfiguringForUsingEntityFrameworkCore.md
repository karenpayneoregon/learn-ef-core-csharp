# Configuring a new project

Rather than placing all code into a front-end project for desktop applications create a class project instead.

Add the proper Entity Framework Core package. 

For instance, for SQL-Server using the following NuGet package

```
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

For Oracle

```
Install-Package Microsoft.EntityFrameworkCore
```

:warning: To be safe, avoid preview versions.

# Database Providers

*EF Core providers are built by a variety of sources. Not all providers are maintained as part of the Entity Framework Core Project. When considering a provider, be sure to evaluate quality, licensing, support, etc. to ensure they meet your requirements. Also make sure you review each provider's documentation for detailed version compatibility information.*

See all providers [here](https://docs.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli)

# Steps

- Design your database schema in SSMS for SQL-Server, Toad for Oracle etc.
- Write SQL for all operations your application will needed. These queries not only validate a schema can take care of business requirements but can also serve as test against EF Core operations.
- Create a class project for EF Core operations
- Add the appropriate NuGet EF Core package.
- There may be a need to install additional packages such as one or all and others dependent on what functionality is needed. The Configuration packages are for reading appsettings.json while the Relational package may be needed to work with metadata.
  - Microsoft.Extensions.Configuration
  - Microsoft.Extensions.Configuration.Binder
  - Microsoft.Extensions.Configuration.EnvironmentVariable
  - Microsoft.Extensions.Configuration.FileExtensions
  - Microsoft.Extensions.Configuration.Json
  - Microsoft.EntityFrameworkCore.Relational
- Reverse engineer the database using [dotnet cli](https://docs.microsoft.com/en-us/ef/core/cli/dotnet), [EF Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools) or another method perhaps from one of these [tools](https://docs.microsoft.com/en-us/ef/core/extensions/).
- Create folders for classes outside of the DbContext and models such as extension methods
- Create a unit test project for testing code in the above class project
