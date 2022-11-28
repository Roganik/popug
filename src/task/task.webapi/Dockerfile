FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["task.webapi/task.webapi.csproj", "task.webapi/"]
RUN dotnet restore "task.webapi/task.webapi.csproj"
COPY . .
WORKDIR "/src/task.webapi"
RUN dotnet build "task.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "task.webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "task.webapi.dll"]
