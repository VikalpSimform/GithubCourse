# Users API

A simple RESTful API built with ASP.NET Core 7.0 for managing users. This project demonstrates modern .NET web API development using minimal APIs, dependency injection, validation, and comprehensive error handling.

## Features

- 🚀 **RESTful API Endpoints** - CRUD operations for user management
- 📝 **Swagger/OpenAPI Documentation** - Interactive API documentation and testing
- ✅ **Input Validation** - Data annotations and custom validation service
- 🛡️ **Global Exception Handling** - Centralized error handling middleware
- 📦 **Dependency Injection** - Clean service architecture
- 📊 **Structured Logging** - Console and debug logging support
- 🎯 **Typed Results** - Strongly-typed HTTP responses

## Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) or higher
- A code editor (Visual Studio, VS Code, or Rider)

## Getting Started

### Installation

1. Clone the repository:
```bash
git clone <repository-url>
cd GithubCourse
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

The API will start at `http://localhost:5000` (or `https://localhost:5001` for HTTPS).

### Accessing Swagger UI

When running in development mode, navigate to:
```
http://localhost:5000/swagger
```

This provides an interactive interface to explore and test the API endpoints.

## API Endpoints

### Get All Users
```http
GET /users
```

**Response:**
```json
{
  "data": [
    {
      "id": 1,
      "name": "Alice",
      "email": "alice@example.com"
    },
    {
      "id": 2,
      "name": "Bob",
      "email": "bob@example.com"
    }
  ],
  "success": true
}
```

### Get User by ID
```http
GET /users/{id}
```

**Parameters:**
- `id` (integer) - User ID

**Success Response (200):**
```json
{
  "data": {
    "id": 1,
    "name": "Alice",
    "email": "alice@example.com"
  },
  "success": true
}
```

**Error Response (404):**
```json
{
  "code": 404,
  "message": "User not found",
  "success": false
}
```

### Create User
```http
POST /users
```

**Request Body:**
```json
{
  "name": "Charlie",
  "email": "charlie@example.com"
}
```

**Validation Rules:**
- `name`: Required, 2-100 characters
- `email`: Required, valid email format

**Success Response (201):**
```json
{
  "data": {
    "id": 3,
    "name": "Charlie",
    "email": "charlie@example.com"
  },
  "success": true
}
```

**Validation Error Response (400):**
```json
{
  "code": 400,
  "message": "Validation failed",
  "details": [
    "The Email field is not a valid e-mail address."
  ],
  "success": false
}
```

## Project Structure

```
GithubCourse/
├── Endpoints/
│   └── UserEndpoints.cs       # API endpoint definitions
├── Middleware/
│   └── ExceptionMiddleware.cs # Global exception handler
├── Models/
│   └── User.cs                # User model and DTOs
├── Services/
│   ├── IUserService.cs        # User service interface
│   ├── UserService.cs         # User service implementation
│   └── ValidationService.cs   # Validation service
├── Program.cs                 # Application entry point
├── HelloWorld.csproj          # Project file
└── SumCalculator.cs           # Utility class
```

## Technologies Used

- **ASP.NET Core 7.0** - Web framework
- **Minimal APIs** - Lightweight endpoint routing
- **Swagger/OpenAPI** - API documentation
- **Data Annotations** - Model validation
- **Dependency Injection** - Service management
- **Serilog-style Logging** - Structured logging

## Architecture

### Services Layer
- **IUserService/UserService**: Manages user data operations (in-memory storage)
- **IValidationService/ValidationService**: Handles model validation using data annotations

### Middleware
- **ExceptionMiddleware**: Catches unhandled exceptions and returns consistent error responses

### Response Format
All API responses follow a consistent structure:
- Success responses: `{ data: T, success: true }`
- Error responses: `{ code: int, message: string, success: false }`
- Validation errors include additional `details` array

## Development

### Running in Development Mode
```bash
dotnet run --environment Development
```

In development mode:
- Swagger UI is enabled
- Detailed error messages are returned
- HTTPS redirection is disabled

### Running in Production Mode
```bash
dotnet run --environment Production
```

In production mode:
- Swagger UI is disabled
- HTTPS redirection is enabled
- Error details are hidden from responses

### Building the Project
```bash
dotnet build
```

### Running Tests
```bash
dotnet test
```

## Data Storage

Currently, the application uses **in-memory storage** with seed data. Users are stored in a `List<User>` and will be reset when the application restarts.

For production use, consider integrating:
- Entity Framework Core with SQL Server/PostgreSQL
- Azure Cosmos DB
- Any other persistent data store

## Future Enhancements

- [ ] Add update (PUT/PATCH) user endpoint
- [ ] Add delete user endpoint
- [ ] Implement persistent database storage
- [ ] Add authentication and authorization
- [ ] Add pagination for user list
- [ ] Add search and filtering capabilities
- [ ] Implement unit and integration tests
- [ ] Add rate limiting
- [ ] Add caching layer

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contact

For questions or feedback, please open an issue in the repository.
