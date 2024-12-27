# Material Purchase

This project is part of a master's thesis focused on designing and implementing an architecture for a material ordering
system.

## Running the project

### Requirements

To successfully run the project, you need to have the following tools installed on your machine:

- .NET 8.0 SDK (https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Docker (https://www.docker.com/)
- sqlpackage (https://learn.microsoft.com/en-us/sql/tools/sqlpackage/sqlpackage-download)
- sqlcmd (https://learn.microsoft.com/en-us/sql/tools/sqlcmd/sqlcmd-utility)

### Set up the database

Run a Docker container with SQL Server by the following command:

```sh
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrongPassword123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

Go to the root directory of the project and build the SQL project by the following command:

```sh
dotnet build "./database/MaterialPurchase.Database.sqlproj"
```

Then publish the database to your local SQL Server by this command:

```sh
sqlpackage /Action:Publish /SourceFile:"./database/bin/Debug/MaterialPurchase.Database.dacpac" /TargetServerName:"localhost" /TargetDatabaseName:"MaterialPurchase" /TargetUser:"sa" /TargetPassword:"YourStrongPassword123" /TargetTrustServerCertificate:True
```

### Seed the database

Then execute the seed script in /scripts/SeedData.sql to populate the database with some data:

```sh
sqlcmd -S localhost -U sa -P YourStrongPassword123 -d MaterialPurchase -i ./scripts/SeedData.sql
```

### Run the project

Go to the root folder of the project and run the following command:

```sh
dotnet run --project "./src/MaterialPurchase/MaterialPurchase.csproj"
```

## License

MIT License (MIT)
