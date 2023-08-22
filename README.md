# Okane
Demo Application for the course Back-end fundamentals

## Installing

Download and Install [.net 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

Open a terminal and run
```bash
dotnet --version
``` 

Install Entity Framework Core tools reference - .NET Core CLI following [this](https://learn.microsoft.com/en-us/ef/core/cli/dotnet#update-the-tools) instructions.

### Build
```bash
dotnet build
```
### Run Tests
```bash
dotnet test
```
### Run WebApi
```bash
dotnet run --project Okane.WebApi/Okane.WebApi.csproj
```
### Install tools
```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```
### Verify instalation
```bash
dotnet ef
```

## Development Environment

* I would recommend [Rider](https://www.jetbrains.com/es-es/rider/download)
* The project also works with [VS Code](https://code.visualstudio.com/download)

## VS Code Extensions

* [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
* [.NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer)
* [UMLet](https://marketplace.visualstudio.com/items?itemName=TheUMLetTeam.umlet)

