# New Zealand Walks API

A RESTful Web API built using **ASP.NET Core** to manage Regions and Walks across New Zealand. This project demonstrates clean backend development practices, database integration, authentication, and authorization.

## 🚀 Tech Stack

- **Backend:** C#, ASP.NET Core Web API
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** JWT (JSON Web Token)
- **Authorization:** Role-Based Authorization
- **Architecture:** Repository Pattern, Dependency Injection
- **API Documentation:** Swagger / OpenAPI
- **Version Control:** Git & GitHub

## ✨ Features

- Manage Regions with CRUD operations.
- Manage Walks with CRUD operations.
- Entity Framework Core database integration.
- Repository Pattern for data access.
- DTOs for request and response models.
- Dependency Injection.
- JWT-based authentication.
- Role-Based Authorization for secured endpoints.
- RESTful API design.
- Swagger API documentation.


## ⚙️ Getting Started

### Prerequisites

- .NET SDK (matching the project target framework)
- SQL Server
- Visual Studio 2022 or VS Code
- Git

### 1. Clone the repository

```bash
git clone https://github.com/wasimk1/NewZealandWalks.API.git
```

### 2. Navigate to the project

```bash
cd NewZealandWalks.API
```

### 3. Configure the database

Update the database connection string in your local configuration with your SQL Server credentials.

**Important:** Never commit actual passwords, JWT secrets, or other sensitive configuration values.

### 4. Apply Entity Framework Core migrations

```bash
dotnet ef database update
```

Run this command from the API project directory, ensuring the EF Core tools and required configuration are available.

### 5. Run the API

```bash
dotnet run --project NZWalks.API
```

### 6. Explore API documentation

Open the Swagger URL displayed in your application output.

## 🔐 Authentication & Authorization

The API uses JWT-based authentication to secure endpoints.

- Authenticate and obtain a JWT token.
- Pass the token in the Authorization header.
- Use role-based authorization to restrict access to protected endpoints.

```http
Authorization: Bearer YOUR_JWT_TOKEN
```

## 🎯 Project Goals

This project demonstrates practical implementation of:

- ASP.NET Core Web API development.
- RESTful API architecture.
- Entity Framework Core and SQL Server.
- Repository Pattern and Dependency Injection.
- JWT Authentication and Role-Based Authorization.
- Backend application design and maintainability.

## 👨‍💻 Author

**Wasim Khan**

Full Stack .NET Developer

[GitHub Profile](https://github.com/wasimk1)

---

⭐ If you find this project useful, feel free to explore the repository!
