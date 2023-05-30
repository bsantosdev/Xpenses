#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Xpenses.API/Xpenses.Api.csproj", "Xpenses.API/"]
COPY ["Xpenses.Application/Xpenses.Application.csproj", "Xpenses.Application/"]
COPY ["Xpenses.Domain/Xpenses.Domain.csproj", "Xpenses.Domain/"]
COPY ["Xpenses.Contracts/Xpenses.Contracts.csproj", "Xpenses.Contracts/"]
COPY ["Xpenses.Infrastructure/Xpenses.Infrastructure.csproj", "Xpenses.Infrastructure/"]
RUN dotnet restore "Xpenses.API/Xpenses.Api.csproj"
COPY . .
WORKDIR "/src/Xpenses.API"
RUN dotnet build "Xpenses.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Xpenses.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Xpenses.Api.dll"]