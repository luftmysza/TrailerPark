# TrailerPark – OMDb Wrapper API

## Overview

This application was developed as part of a **DevOps lab course** in a Computer Science undergraduate program, with a strong emphasis on **containerization, infrastructure awareness, and deployment workflows**.

The project implements a **wrapper around the OMDb API**, adding a caching layer and providing a clean, extensible backend foundation. It follows **Clean Architecture principles**, clearly separating responsibilities across layers to keep the system modular, testable, and maintainable.

For persistence, the application uses **PostgreSQL** in production environment, while supporting lighter configurations (such as in-memory databases) during development. The entire system is **fully Dockerized**, with the API and database running in separate containers, enabling reproducible local development and deployment-ready setups.

---

## Architecture Overview

- **API Layer** – HTTP endpoints and request handling  
- **Application Layer** – Business logic and use cases  
- **Infrastructure Layer** – External integrations (OMDb, PostgreSQL, repositories)  
- **Domain/Core Layer** – Core models

---

## Prerequisites

- Docker
- Docker Compose
- .NET SDK (for running EF Core migrations from the host)

---

## Getting Started

### 1. Build the Docker images

`docker compose build`

### 2. Start the database container

`docker compose up --detach db`

### 3. Apply database migrations

Apply EF Core migrations while the database is running and the API is stopped:

`ASPNETCORE_ENVIRONMENT=Staging dotnet ef database update --project TrailerPark.Infrastructure --startup-project TrailerPark.API --context AppDbContext`

This step initializes the database schema before the API starts.

### 4. Start the API container

`docker compose up --detach api`

The API should now be running and connected to PostgreSQL.