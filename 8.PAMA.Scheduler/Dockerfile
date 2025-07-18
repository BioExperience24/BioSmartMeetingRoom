# Use the official .NET 8 SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY PAMA_SMeet_Room.sln ./
COPY 1.PAMA.Razor.Views/*.csproj ./1.PAMA.Razor.Views/
COPY 2.Web.API.Controllers/*.csproj ./2.Web.API.Controllers/
COPY 3.BusinessLogic.Services/*.csproj ./3.BusinessLogic.Services/
COPY 4.Data.ViewModels/*.csproj ./4.Data.ViewModels/
COPY 5.Helpers.Consumer/*.csproj ./5.Helpers.Consumer/
COPY 6.Repositories/*.csproj ./6.Repositories/
COPY 7.Entities.Models/*.csproj ./7.Entities.Models/
COPY 8.PAMA.Scheduler/*.csproj ./8.PAMA.Scheduler/

RUN dotnet clean
RUN dotnet restore

# Copy all files and publish
COPY . ./
RUN dotnet publish 8.PAMA.Scheduler/8.PAMA.Scheduler.csproj -c Release -o out

# Use a smaller runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install dependencies for SkiaSharp
RUN apt-get update && apt-get install -y \
    libfontconfig1 \
    libfreetype6 \
    libharfbuzz0b \
    libx11-6 \
    libxext6 \
    libxrender1 \
    libxcb1 \
    libx11-xcb1 \
    libxrandr2

COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "8.PAMA.Scheduler.dll", "--urls", "http://+:80"]
