# ToDo.Backend
- A backend project that implements minial Todo APIs.
- With support for multiple storage methods (in-memory and SQLite database), allowing easy switching between storage implementations.
- ASP.NET 8 

## Features
- RESTful API.
- Support easy switch between in-memory and database storage (SQLite) service.
- DTOs (Data Transfer Objects) for clean separation between API and business logic.
- Mapping utilities to convert between domain models and DTOs.
- Dependency injection for flexible service management.
- Entity Framework Core for database management.

## Project Structure
```plaintext
├── Data
│   ├── Migrations
│   │   ├── <Migration Files>
│   ├── DataExtensions.cs
│   ├── TodoContext.cs
├── Dtos
│   ├── CreateTodoDto.cs
│   ├── TodoDto.cs
│   ├── UpdateTodoDto.cs
├── Endpoints
│   ├── TodoEndpoints.cs
├── Entities
│   ├── Todo.cs
├── Interface
│   ├── ITaskService.cs
├── Mapping
│   ├── TodoMapping.cs
├── Services
│   ├── DatabaseService.cs
│   ├── InMemoryService.cs
├── appsettings.json
├── Program.cs
```

## Prerequisites
- .NET 8 SDK
- SQLite (for database storage)

## Setup Instructions

### Clone the Repository
```bash
git clone <repository-url>
cd backend_todos
```

### Configure the Environment
1. Add your connection string to an `.env` file:
   ```env
   ConnectionStrings__Todo="Data Source=Todo.db"
   ```

2. Ensure the `appsettings.json` references this connection string:
   ```json
   {
     "ConnectionStrings": {
       "ToDo": "Data Source=Todo.db"
     }
   }
   ```

### Database Setup
Install `dotnet-ef` tool by running following command:
```bash
dotnet tool install --global dotnet-ef --version 9.0.1
```
Run the following command to initialize the database:
```bash
dotnet ef database update
```

### Run the Application
```bash
dotnet run
```
The API will be available at `http://localhost:5286` by default.

## API Endpoints

### GET 
#### GET`/todos`
Fetch all todo items.
#### GET `/todos/done`
Fetch all completed todo items and order by due date.
#### GET `/todos/undone`
Fetch all uncompleted todo items and order by due date.

### POST `/todos`
Create a new todo item.
- Request Body:
  ```json
  {
    "name": "Learn C#",
    "dueDate": "2025-11-30"
  }
  ```

### PUT `/todos/{id}`
Update an existing todo item.
- Request Body:
  ```json
  {
    "name": "Learn .NET",
    "dueDate": "2025-12-01",
    "isCompleted": true,
    "isPinned":true
  }
  ```

### DELETE `/todos/{id}`
Delete a todo item by ID.

## Switching Storage Methods
To switch between in-memory and database storage, update the `Program.cs` file:

### For In-Memory Storage
```csharp
builder.Services.AddSingleton<ITaskService, InMemoryService>();
```

### For Database Storage
```csharp
builder.Services.AddScoped<ITaskService, DatabaseService>();
```

## Contributing
Contributions are welcome! Feel free to submit a pull request or open an issue for discussion.

## License
This project is licensed under the MIT License. See the LICENSE file for details.


