FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["sso.webapi/sso.webapi.csproj", "sso.webapi/"]
RUN dotnet restore "sso.webapi/sso.webapi.csproj"
COPY . .
WORKDIR "/src/sso.webapi"
RUN dotnet build "sso.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sso.webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sso.webapi.dll"]
