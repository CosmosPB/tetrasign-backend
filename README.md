# FYI

Commands
```bash
dotnet new webapi -minimal -au IndividualB2C 
dotnet new classlib --name TetraSign.Core  
dotnet new sln --name tetrasign-backend 
dotnet add TetraSign.WebApi/TetraSign.WebApi.csproj reference TetraSign.WebApi/TetraSign.WebApi.csproj
dotnet add TetraSign.Core/TetraSign.Core.csproj reference TetraSign.Core/TetraSign.Core.csproj
dotnet add TetraSign.WebApi/TetraSign.WebApi.csproj reference TetraSign.Core/TetraSign.Core.csproj
dotnet sln tetrasign-backend.sln add TetraSign.WebApi/TetraSign.WebApi.csproj
dotnet sln tetrasign-backend.sln add TetraSign.Core/TetraSign.Core.csproj
dotnet run --project TetraSign.WebApi/TetraSign.WebApi.csproj --environment Development
dotnet build --project TetraSign.Core/TetraSign.Core.csproj
docker-compose build --no-cache
docker-compose up -d
docker-compose down
```

nuget.config example

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <add key="github" value="https://nuget.pkg.github.com/CosmosPB/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <github>
            <add key="Username" value="USERNAME" />
            <add key="ClearTextPassword" value="PAT" />
        </github>
    </packageSourceCredentials>
</configuration>
```