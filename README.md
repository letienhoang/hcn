# E-Commerce Project (ABP Framework 6.0)

## Introduction

This is an E-Commerce project developed using **ABP Framework 6.0**, with **.NET Core 6.0** for the backend and **Angular 14** for the frontend. The project was initially designed to build an e-commerce system but is currently on hold.

## Technologies Used

- **Backend:** ASP.NET Core 6.0, ABP Framework 6.0
- **Frontend:** Angular 14
- **Database:** SQL Server (expandable to other database management systems)
- **Authentication & Authorization:** IdentityServer
- **Dependency Injection:** Built-in DI of ABP Framework
- **ORM:** Entity Framework Core

## Project Structure

The project follows the standard ABP Framework structure:

- **src/**: Contains the main source code for both backend and frontend
- **aspnet-core/**: Contains the backend built with ASP.NET Core
- **angular/**: Contains the frontend built with Angular 14
- **migrations/**: Contains Entity Framework Core migrations
- **docs/**: Documentation for usage and development

## How to Run the Project

### 1. Install System Requirements

Before running the project, ensure your system has the required tools installed:
- .NET SDK 6.0
- Node.js & npm
- Angular CLI
- SQL Server
- ABP CLI (Install using the following command: `dotnet tool install -g Volo.Abp.Cli`)

If you have already installed ABP CLI, you can update it to the latest version using:
```sh
dotnet tool update -g Volo.Abp.Cli
```

### 2. Set Up the Database

```sh
cd aspnet-core
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Start the Backend

```sh
cd aspnet-core
dotnet run
```

### 4. Start the Frontend

```sh
cd angular
npm install
ng serve --open
```

Once successfully started, you can access the application at `http://localhost:4200/`.

## Useful Commands for Working with ABP

### Generate a New Module
```sh
dotnet abp generate module MyNewModule
```

### Run the Project in Watch Mode (auto-reloads on changes)
```sh
dotnet watch run
```

### Update ABP Packages to the Latest Version
```sh
dotnet abp update
```

## References

For more details, check the official ABP Framework documentation: [ABP Documentation](https://abp.io/docs/6.0)

## Notes

This project is currently **on hold** and may be resumed in the future. If you are interested or would like to contribute, feel free to contact or fork this repository to continue development.

## Contact

If you have any questions or suggestions, please contact via email (ltienhoang2@gmail.com) or create an issue in this repository.

---
Thank you for your interest in this project!

