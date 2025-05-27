# 📦 CompanyAPI

A full-stack web application for managing company records, built with **ASP.NET Core**, **Angular**, and **SQL Server**.  
The project is containerized using Docker for seamless development and deployment.

---

## 🚀 Features

- ✅ Backend: ASP.NET Core Web API with Entity Framework Core
- ✅ Frontend: Angular application served via Nginx
- ✅ Database: SQL Server 2022
- ✅ Containerization: Docker & Docker Compose
- ✅ Migrations: Automatic execution of SQL scripts on startup

---

## 🔗 API Endpoints

The backend API exposes the following main endpoints under the `/api` route:

| HTTP Method | Endpoint           | Description                         |
|-------------|--------------------|-----------------------------------|
| GET         | `/company`         | Retrieves a list of companies with optional pagination parameters (`limit`, `offset`). |
| GET         | `/company/{id}`    | Retrieves details of a single company by its ID. |
| GET         | `/company/isin/{isin}`    | Retrieves details of a single company by its ISIN. |
| POST        | `/company`         | Creates a new company record. |
| PUT         | `/company/{id}`    | Updates an existing company record by ID. |

> Pagination example:  
> `/company?limit=10&offset=0` returns the first 10 companies.

---

## 🛠️ Prerequisites

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

---

## 📦 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/ace333/CompanyAPI.git
cd CompanyAPI
```

### 2. Build and Run with Docker Compose

```bash
docker-compose up --build
```

This will:

- Build backend and frontend apps
- Start SQL Server
- Execute the database migration script
- Serve the Angular frontend via Nginx

### 3. Access the application

🧑‍💻 Frontend: http://localhost:4200

