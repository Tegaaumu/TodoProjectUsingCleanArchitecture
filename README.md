# Todo Project using Clean Architecture

A robust Todo Application built with **.NET 8 Web API** following the principles of **Clean Architecture**. This project uses **PostgreSQL** as the database and **Dapper** for lightweight, high-performance data access.

## 🚀 Features

- **Clean Architecture**: Separated into Domain, Application, Infrastructure, and Presentation layers.
- **Dapper ORM**: Fast and efficient database operations.
- **PostgreSQL**: Production-ready relational database.
- **Database Initialization**: Automatic table creation on startup.
- **Swagger UI**: Interactive API documentation.

## 🏗️ Architecture Overview

The project is structured into several layers to ensure separation of concerns and maintainability:

- **Domain/Contract**: Defines the core data structures and request/response models.
- **Application**: Contains business logic, service interfaces, and repository interfaces.
- **Infrastructure (Database)**: Implements database-specific logic, connection factories, and repository implementations using Dapper.
- **Presentation (API)**: ASP.NET Core Web API controllers and mapping logic.

## 🛠️ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 15+](https://www.postgresql.org/download/)

## ⚙️ Setup Instructions

### 1. Database Configuration
Ensure PostgreSQL is running and create a database named `TodoList`. You also need a user with create permissions on the `public` schema.

Update the `appsettings.json` file in the `TodoProjectUsingCleanArchitecture` project with your connection string:

```json
"Database": {
  "ConnectionString": "Host=localhost;Port=5432;Database=TodoList;Username=YOUR_USERNAME;Password=YOUR_PASSWORD"
}
```

### 2. Run the Application
Open your terminal in the project root and run:

```bash
dotnet run --project TodoProjectUsingCleanArchitecture
```

The API will be available at `https://localhost:7151` (or the port specified in `launchSettings.json`).

### 3. Access Swagger
Navigate to `https://localhost:7151/swagger` to view the interactive API documentation.

## 🛣️ API Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| **GET** | `/api/TodoList` | Retrieve all todo items. |
| **GET** | `/api/TodoList/{id}` | Retrieve a specific todo item by ID. |
| **POST** | `/api/TodoList` | Create a new todo item. |
| **PUT** | `/api/TodoList/{id}` | Update an existing todo item. |
| **DELETE** | `/api/TodoList/{id}` | Delete a todo item. |

## 🧪 Technologies Used

- **ASP.NET Core 8.0**
- **Dapper**
- **Npgsql** (PostgreSQL driver)
- **Swagger/OpenAPI**
- **Dependency Injection**
