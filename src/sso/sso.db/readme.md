# EF Hints

## Install dotnet-ef

`dotnet tool install --global dotnet-ef`

## Add migration

```
cd to this folder
dotnet ef migrations add migration_name --project sso.db.migrations.csproj
```

## Apply migration to your db

`dotnet ef database update`