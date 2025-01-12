# BookHub API

BookHub is a RESTful API developed using .NET 9 Minimal APIs. It manages **authors** and **books**, including their relationships.

---

## Features

- **Author Management**:
  - Create, retrieve, update, and delete authors.
  - Retrieve all books written by an author.
- **Book Management**:
  - Create, retrieve, update, and delete books.
- Adheres to clean architecture principles.
- Provides a Swagger UI for testing and exploring the API endpoints.

---

## Technologies Used

- **.NET 9 Minimal API**
- **Entity Framework Core**
- **SQLite** (for development)
- **AutoMapper**
- **Swagger/OpenAPI**

---

## Project Structure

```
BookHub
|
├── Context
│   └── ApplicationDbContext.cs         # Database context
|
├── DTOS
│   ├── AuthorDto.cs                   # Data Transfer Object for Author
│   ├── AuthorDtoCreate.cs             # DTO for creating Authors
│   ├── AuthorDtoUpdate.cs             # DTO for updating Authors
│   ├── BookDto.cs                     # Data Transfer Object for Book
│   ├── BookDtoCreate.cs               # DTO for creating Books
│   ├── BookDtoUpdate.cs               # DTO for updating Books
|
├── EndpointHandlers
│   ├── AuthorHandlers.cs              # Handlers for Author-related endpoints
│   ├── BookHandlers.cs                # Handlers for Book-related endpoints
|
├── Extensions
│   └── EndpointRouteBuilderExtensions.cs # Extension methods for routing
|
├── Models
│   ├── Author.cs                      # Author entity
│   ├── Book.cs                        # Book entity
|
├── Profiles
│   └── BookHubProfile.cs              # AutoMapper profile
|
├── Migrations                         # EF Core migrations folder
|
├── appsettings.json                   # Application configuration
├── Program.cs                         # Application entry point
```

---

## API Endpoints

### Author Endpoints

| HTTP Method | Endpoint           | Description                 |
|-------------|--------------------|-----------------------------|
| GET         | `/author`          | Retrieves all authors       |
| POST        | `/author`          | Creates a new author        |
| PUT         | `/author`          | Updates an existing author  |
| DELETE      | `/author`          | Deletes an author           |
| GET         | `/author/books`    | Retrieves books by author   |
| GET         | `/author/{id}`     | Retrieves author by ID      |

### Book Endpoints

| HTTP Method | Endpoint           | Description                 |
|-------------|--------------------|-----------------------------|
| GET         | `/book`            | Retrieves all books         |
| POST        | `/book`            | Creates a new book          |
| PUT         | `/book`            | Updates an existing book    |
| DELETE      | `/book`            | Deletes a book              |
| GET         | `/book/{id}`       | Retrieves book by ID        |

---

## Prerequisites

- .NET 9 SDK
- SQLite installed (optional, database is created automatically if not present)

---

## Getting Started

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd BookHub
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Apply migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open the Swagger UI:
   Navigate to `https://localhost:7029/swagger` in your browser to explore and test the API.

---

## Future Improvements

- Add authentication and authorization.
- Implement advanced search and filtering for books and authors.
- Support for pagination in GET endpoints.
