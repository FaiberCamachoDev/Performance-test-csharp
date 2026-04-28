# 🏟️ SportManager — Sports Venue Booking System

A web-based system built with ASP.NET Core MVC to manage users, sports venues, and reservations at a sports complex.

---

## Tech Stack

- **ASP.NET Core MVC** (.NET 9)
- **Entity Framework Core** with PostgreSQL
- **LINQ** for data querying
- **SMTP** for email notifications
- **Bootstrap Icons** + custom CSS

---

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/) running locally
- An SMTP email account (Gmail or any other provider)

---

## Setup

### 1. Clone the repository

```bash
git clone https://github.com/FaiberCamachoDev/Performance-test-csharp.git
cd Performance-test-csharp
```

### 2. Configure `appsettings.json`

Edit the file with your own credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=sportmanager;Username=postgres;Password=YOUR_PASSWORD"
  },
  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "From": "your-email@gmail.com"
  }
}
```

> **Note:** If you use Gmail, generate an *App Password* from your Google account settings. Do not use your regular password.

### 3. Apply database migrations

```bash
dotnet ef database update
```

This creates the `Users`, `DeportiveSpaces`, and `Reservations` tables in your database.

### 4. Run the project

```bash
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

---

## Features

### 👥 User Management
| Action | Route |
|---|---|
| List all users | `GET /Users` |
| Register new user | `GET /Users/Create` |
| Save user | `POST /Users/Create` |

**Validations:**
- Name, document, phone, and email are required
- Document must be unique (max 10 characters)
- Email must be unique and valid
- Phone max 10 digits

---

### 🏟️ Venue Management
| Action                         | Route |
|--------------------------------|---|
| List all venues                | `GET /DeportiveSpace` |
| Filter by type (not avaliable) | `GET /DeportiveSpace?type=Football` |
| Register new venue             | `GET /DeportiveSpace/Create` |
| Save venue                     | `POST /DeportiveSpace/Create` |

**Supported types:** Football, Basketball, Tennis, Gym, Swimming, Volleyball

**Validations:**
- Venue name must be unique
- Capacity must be between 1 and 100

---

### 📅 Reservation Management
| Action | Route |
|---|---|
| List all reservations | `GET /Reservations` |
| New reservation | `GET /Reservations/Create` |
| Save reservation | `POST /Reservations/Create` |
| Cancel reservation | `POST /Reservations/Cancel/{id}` |

**Possible statuses:** `Active` → `Cancelled`

**Validations:**
- A venue cannot have overlapping reservations on the same day
- Only `Active` reservations can be cancelled
- A confirmation email is sent to the user upon booking creation

---

---

## Diagrams

Class and use case diagrams are available in the `/docs` folder of the repository.

---

## Author
Faiber Camacho,
Developed as a C# performance assessment — RIWI.