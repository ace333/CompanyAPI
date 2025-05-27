FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY CompanyAPI.Application/. ./CompanyAPI.Application
COPY CompanyAPI.Domain/. ./CompanyAPI.Domain
COPY CompanyAPI.Infrastructure/. ./CompanyAPI.Infrastructure
COPY CompanyAPI.Api/. ./CompanyAPI.Api

RUN dotnet publish CompanyAPI.Api/CompanyAPI.Api.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "CompanyAPI.Api.dll"]