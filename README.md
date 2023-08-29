# Okane
Demo Application for the course Back-end fundamentals

## Course Outline
### Day 1
1. [.NET Project Structure and Project Types](https://github.com/orodriguez/Okane/compare/0-start...1-create-solution-and-tests)
    * Basic dotnet CLI commands.
    * Editors/IDEs for .net/C#
2. [Initial Project Architecture](https://github.com/orodriguez/Okane/compare/1-create-solution-and-tests...2-architecture)
   * UML Illustrating Projects and Dependencies
3. [Register Expense](https://github.com/orodriguez/Okane/compare/2-architecture...3-register-expense)
   * Unit Testing with xUnit
   * Service objects
   * Repository objects
   * Testing using Fake Implementation
4. [Retrieve all Expenses](https://github.com/orodriguez/Okane/compare/3-register-expense...4-retrieve-all-expenses)
   * Enumerables in C#
5. [WebApi Project](https://github.com/orodriguez/Okane/compare/4-retrieve-all-expenses...5-web-api)
    * .NET 7 WebApi Project
    * Controllers
    * Routing
    * HTTP Verbs
    * Dependency Injection Configuration (DIP Principle)
    * Swagger and Swagger UI
6. [Refactor: Introduce IExpensesService interface](https://github.com/orodriguez/Okane/compare/5-web-api...6-refactor)
    * Using interfaces to facilitate testing with test doubles
7. [Contracts Project](https://github.com/orodriguez/Okane/compare/6-refactor...7-contracts-dtos)
    * Data Transfer Objects
    * Requests: Input DTOs
    * Responses: Output DTOs
8. [Storage Project](https://github.com/orodriguez/Okane/compare/7-contracts-dtos...8-storage-inmemory)
    * Moving Storage access to a separated project
9. [Filter Expenses by Category](https://github.com/orodriguez/Okane/compare/8-storage-inmemory...9-filter-by-category)
    * LINQ to Object Collections
    * Query String Parameters in Controllers
### Day 2
10. [Get Expense by Id](https://github.com/orodriguez/Okane/compare/9-filter-by-category...10-get-by-id)
    * Route Parameters
11. [Get Expense, Id Not Found](https://github.com/orodriguez/Okane/compare/10-get-by-id...11-get-by-id-response-types)
    * Nullable Types
    * HTTP Response Codes
    * ProduceResponseType Attribute
    * ActionResult Subtypes
12. [Validate Expense on Registration](https://github.com/orodriguez/Okane/compare/11-get-by-id-response-types...12-validate-expenses-on-creation)
    * DataAnnotations Attributes
    * ModelStateDictionary
    * 400 Bad Request Response
13. [UnitTesting Validations](https://github.com/orodriguez/Okane/compare/12-validate-expenses-on-creation...13-test-validations)
    * Generic Validator
    * ValidationContext
14. [Storage.EntityFramework Project](https://github.com/orodriguez/Okane/compare/13-test-validations...14-ef-repository)
    * DbContext and DbSets
    * DbContext Configuration
    * Installing dotnet ef CLI tools
    * Create Initial Migration
    * Applying Initial Migration to Database
### Day 3
15. [Expense Description](https://github.com/orodriguez/Okane/compare/14-ef-repository...15-expense-description)
    * Applying Changes Iteratively with Migrations
16. [Expense Invoice URL](https://github.com/orodriguez/Okane/compare/15-expense-description...16-expense-invoice-url)
17. [Expense Created Date](https://github.com/orodriguez/Okane/compare/16-expense-invoice-url...17-add-created-date)
    * Deterministic UnitTesting
    * Techniques to make unit tests deterministic
18. [Refactor: Introduce Categories Table](https://github.com/orodriguez/Okane/compare/17-add-created-date...18-category-entity)
    * Database Normalization
    * Refactoring internal Entities without affecting Contract (Orthogonality)
    * Migration Data: Avoiding data lost in Migrations
### Day 4
19. [Create Expenses - Category Does not Exist](https://github.com/orodriguez/Okane/compare/18-category-entity...19-expense-with-non-existing-category)
    * Tupples in C#
    * Result Object Pattern
20. [Bufix: Include Category in Expense](https://github.com/orodriguez/Okane/compare/19-expense-with-non-existing-category...20-bug-fix-include-category-in-repository-methods)
    * Eager Loading
    * Lazy Loading
21. [Integration Tests](https://github.com/orodriguez/Okane/compare/20-bug-fix-include-category-in-repository-methods...21-integration-tests)
    * Types of Unit Testing
    * Integration Testing
    * WebApplicationFactory
    * HttpClient
22. [Refactor: Move Validations to Service](https://github.com/orodriguez/Okane/compare/21-integration-tests...22-move-validations-to-service)
    * Thin Controllers
23. [Delete Expense](https://github.com/orodriguez/Okane/compare/22-move-validations-to-service...23-delete-expense)
24. [Update Expense](https://github.com/orodriguez/Okane/compare/23-delete-expense...24-update-expense)
### Day 4
25. [Sign Up](https://github.com/orodriguez/Okane/compare/24-update-expense..25-signup)
    * Password Hashers: BCrypt
26. [Token Generation](https://github.com/orodriguez/Okane/compare/25-signup...26-generate-token)
    * Jwt Token Generation
    * Claims
    * Token Signature
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

