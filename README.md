# Expense Tracker API

This project is an Expense Tracker API built with .NET 8, implementing Clean Architecture principles. It was created as a practice exercise to explore .NET 8 features and Clean Architecture concepts.

## Prerequisites

- .NET 8 SDK
- PostgreSQL database
- Docker (optional, for running PostgreSQL)

## Setup

1. Clone the repository:    
    - git clone [\[repo-url\]](https://github.com/Jaleel-VS/ExpenseTrackerAPI)
    - cd ExpenseTrackerAPI
2. Update the connection string in `appsettings.json` to point to your PostgreSQL database.
3. Run database migrations:
dotnet ef database update
4. Run the application:
dotnet run
## Features

- User authentication
- Expense management
- Category management

## API Endpoints

- POST /api/auth/register - Register a new user
- POST /api/auth/login - Login and receive JWT token
- GET /api/expenses - Get all expenses for the authenticated user
- POST /api/expenses - Create a new expense
- GET /api/categories - Get all categories for the authenticated user
- POST /api/categories - Create a new category

## Architecture

This project follows Clean Architecture principles, with clear separation between:
- API (Presentation layer)
- Application (Use cases)
- Domain (Business entities)
- Infrastructure (Data access and external services)

## Technologies Used

- .NET 8
- Entity Framework Core
- PostgreSQL
- JWT for authentication

## Contributing

This is a personal practice project, but feel free to fork and experiment!

## License

[MIT License](LICENSE)