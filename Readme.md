## Add Migration
dotnet ef migrations add initializeDb -s BE.Presentation.API -p BE.Infrastructure.DataContext

## Update DB
dotnet ef database update -s BE.Presentation.API -p BE.Infrastructure.DataContext