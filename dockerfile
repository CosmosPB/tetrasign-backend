FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY ./TetraSign.Core ./TetraSign.Core
COPY ./TetraSign.WebApi ./TetraSign.WebApi
COPY ./tetrasign-backend.sln ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "TetraSign.WebApi.dll"]