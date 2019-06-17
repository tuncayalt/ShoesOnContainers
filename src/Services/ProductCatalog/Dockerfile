FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /code
COPY . .
RUN dotnet restore
RUN dotnet publish --output /out/ --configuration Release

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS runtime
COPY --from=build /out /app/
WORKDIR /app

ENTRYPOINT ["dotnet", "ProductCatalog.dll"]