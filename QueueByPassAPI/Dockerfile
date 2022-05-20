#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["QueueByPassAPI/QueueByPassAPI.csproj", "QueueByPassAPI/"]
RUN dotnet restore "QueueByPassAPI/QueueByPassAPI.csproj"
COPY . .
WORKDIR "/src/QueueByPassAPI"
RUN dotnet build "QueueByPassAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueueByPassAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueueByPassAPI.dll"]