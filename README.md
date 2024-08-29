# Plottar (Anonymous Jobs For Everyone)

# Index

- [Overview](#overview)
- [Key Technologies](#key-technologies)
- [Project Structure](#project-structure)
- [Folder Descriptions](#folder-description)
- [Environment Variables](#environment-variables)

## Overview

Plottar is a web application built with Asp.Net Core using C#, Postgres among other technologies. This project aims to ease the Job posting by anonymous users for the tech industry. People who visit the site will be able to find interesting jobs, filter by what they are looking for and apply by clicking the link of the position.

<div style="margin: 3rem auto; max-width: 300px;">
  <img src="https://firebasestorage.googleapis.com/v0/b/backer-bb647.appspot.com/o/clean-arch.png?alt=media&token=a4cbb745-a865-4cb2-aada-6f5c1dc5dc5a" alt="Clean architecture">
</div>

## Key Technologies

- **.NET 8 with C#:** The project is built using .NET 8, with C# as the primary programming language.

## Project Structure

The solution is organized into the following folders, reflecting the principles of Clean Architecture:

```mathematica
Backer
├── Backer.Api
├── Backer.Contracts
├── Backer.Infrastructure
├── Backer.Application
└── Backer.Domain
└── Endpoints
└── Docs

```

### Folder Descriptions:

1. **Backer.Api:**

   - Contains the web API endpoints.
   - Serves as the entry point for client interactions.
   - References the `Application` layer.

2. **Backer.Contracts:**

   - Defines the contracts (DTOs, request/response models) used for communication between the API and clients.
   - Also references the `Application` layer.

3. **Backer.Infrastructure:**
   - Implements the infrastructure concerns, such as data access, external service integrations, and repository patterns.
   - References both the `Application` and `API` layers.
4. **Backer.Application:**
   - Contains the application logic, use cases, and service interfaces.
   - References the `Domain` layer, implementing business rules and orchestrating tasks.
5. **Backer.Domain:**
   - Defines the core domain model, including entities, value objects, and domain services.
   - Independent of other layers, ensuring that the business logic is decoupled from infrastructure and application concerns.
6. **Endpoints:**
   - Contains all request endpoints documentations using http files
7. **Docs:**
   - Extensive documentation about projects and libraries

## Environment Variables

### JWT_SECRET

**Description:**  
The `JWT_SECRET` environment variable is a critical part of the security configuration for Plottar. It holds the secret key used for signing JWT (JSON Web Tokens), which are essential for securing API requests and managing user authentication within the application.

**Usage:**  
This secret is accessed by the `JwtGenerator` class in the `Plottar.Infrastructure.Authentication` namespace to generate JWT tokens for authenticated users. The secret ensures that the tokens are securely signed, preventing unauthorized access.

**Setup:**

1. **Initialize User Secrets:**  
   Run the following commands in the terminal at the root of your project:

   ```bash
   dotnet user-secrets init
   ```

2. **Set the JWT_SECRET:**

   ```bash
   dotnet user-secrets set "JWT_SECRET" "your-secret-key"
   ```

   Replace "your-secret-key" with a strong, unique secret string.
