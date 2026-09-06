# DVLD – Driving License Management System

A desktop application built with **.NET (WinForms)** that simulates a real-world **Department of Vehicle and Driving Licenses (DVLD)** system. It manages the full lifecycle of driving licenses — from applicant registration and testing, to issuing, renewing, replacing, and detaining licenses — following a clean **3-Tier Architecture**.

This project was built as a hands-on exercise to apply solid software architecture principles (separation of concerns, layered design) to a real, business-heavy domain rather than a simple CRUD demo. It took roughly **3–4 months** to design and build.

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Database Setup](#-database-setup)
- [Screenshots](#-screenshots)
- [Future Improvements](#-future-improvements)
- [License](#-license)

---

## 🧭 Overview

DVLD is a Windows Forms desktop application that models the core operations of a driving license authority. It covers people/applicant management, driving tests, license issuance (local & international), license renewal/replacement, license detention/release, and user accounts — all backed by a normalized SQL Server database and accessed through a hand-written ADO.NET data layer (no ORM), using parameterized SQL queries for full control over data access and transactions.

## ✨ Features

**Applicants & People**
- Add, search, and update person records (national ID, contact info, address, etc.)
- Attach and manage applicant photos/documents

**Driving Tests**
- Schedule and manage test appointments: Vision Test, Written Test, and Practical (Road) Test
- Record pass/fail results and enforce test-order/eligibility rules before issuing a license

**License Management**
- Issue a new **local** driving license
- Issue a new **international** driving license
- Renew an existing license
- Replace a lost or damaged license
- Detain a license (e.g., due to violations) and release it later
- Track license history and status per driver

**Applications Workflow**
- Create and track license applications (new license, renewal, replacement, international) with status and fees

**Users & Security**
- User accounts with active/inactive status
- Login/authentication screen

> ⚠️ **Note:** Passwords are currently stored as plain text for simplicity. Hashing passwords (e.g., with BCrypt) before storage is listed under [Future Improvements](#-future-improvements).

**Reports**
- Generate reports (e.g., licenses issued, applicants, tests) for record-keeping

> 💡 Adjust this list to exactly match the modules you implemented (e.g., if traffic violations or printable license cards are included).

## 🏗 Architecture

The system follows a classic **3-Tier Architecture**, keeping each layer independent and replaceable:

```
┌───────────────────────────┐
│   Presentation Layer       │  → WinForms UI (11 - DVLD Project)
├───────────────────────────┤
│   Business Logic Layer     │  → Validation & business rules (DVLD - Business Layer)
├───────────────────────────┤
│   Data Access Layer         │  → ADO.NET + Parameterized SQL Queries (DVLD - Dataccess Layer)
├───────────────────────────┤
│   SQL Server Database       │
└───────────────────────────┘
```

- **Presentation Layer** – WinForms UI responsible only for user interaction and displaying data.
- **Business Layer** – Encapsulates business rules, validation, and workflow logic (e.g., a person can't get a license without passing all required tests).
- **Data Access Layer** – Handles all communication with SQL Server via ADO.NET using parameterized inline SQL queries, isolating SQL from the rest of the application.

This separation makes the codebase easier to maintain, test, and extend — for example, the UI could be swapped for a web front-end without touching the business or data layers.

## 🛠 Tech Stack

| Layer            | Technology                     |
|-------------------|----------------------------------|
| UI                | Windows Forms (.NET, C#)        |
| Business Logic    | C# (Class Library)              |
| Data Access       | ADO.NET (parameterized inline SQL queries) |
| Database          | Microsoft SQL Server            |
| Architecture      | 3-Tier Architecture             |

## 📁 Project Structure

```
DVLD-Driving-License-Management-System/
│
├── 11 - DVLD Project/           # Presentation Layer (WinForms UI)
├── DVLD - Business Layer/       # Business Logic Layer
├── DVLD - Dataccess Layer/      # Data Access Layer (ADO.NET)
├── 11 - DVLD Project.sln        # Visual Studio solution file
└── README.md
```

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2019/2022 (or later)
- .NET Framework (see the project's target framework in Visual Studio)
- Microsoft SQL Server (Express or higher) + SQL Server Management Studio (SSMS)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Galal-Alzumaili/DVLD-Driving-License-Management-System.git
   ```
2. **Open the solution**
   - Open `11 - DVLD Project.sln` in Visual Studio.
3. **Restore/set up the database** (see [Database Setup](#-database-setup) below).
4. **Update the connection string**
   - Locate the connection string (typically in the Data Access Layer or an `App.config` file) and point it to your local SQL Server instance.
5. **Build and run**
   - Set `11 - DVLD Project` as the startup project and press `F5`.

## 🗄 Database Setup

A ready-to-run SQL script is included in the repo under `Database/DVLD_Database_Script.sql`. It creates the `DVLD` database with all tables, views, and relationships, plus:
- **Lookup/reference data** the app needs to function: Countries, License Classes, Application Types, and Test Types.
- **One default `admin` account** so you have a working login on first run.

No other sample data (applicants, drivers, licenses, applications...) is included — you're expected to create your own data through the application itself.

1. Open **SQL Server Management Studio (SSMS)** and connect to your local SQL Server instance.
2. Open the file `Database/DVLD_Database_Script.sql` (**File → Open → File...**).
3. Execute the script (press `F5` or click **Execute**). This will create the `DVLD` database with its schema and lookup data.
4. Update the connection string in the app (typically in the Data Access Layer or an `App.config` file) to match your SQL Server instance name.

### Default login

| Username | Password |
|----------|----------|
| `admin`  | `1234`   |

> ⚠️ This is a default account for local testing only — passwords are stored as plain text in the current version (see [Future Improvements](#-future-improvements)). Change this password immediately, and never reuse it in a production environment.

## 🖼 Screenshots

### Login Screen
![Login Screen](docs/screenshots/login.png)

### Main Screen
![Main Screen](docs/screenshots/main.png)

### Manage Users Screen
![Manage Users Screen](docs/screenshots/manage-users.png)

### Change Password Screen
![Change Password Screen](docs/screenshots/change-password.png)

### Issue License Screen
![Issue License Screen](docs/screenshots/issue-license.png)

### Manage Local Applications Screen
![Manage Local Applications Screen](docs/screenshots/manage-local-applications.png)

> 📝 If any image doesn't render, double check the file name and extension match **exactly** (case-sensitive) what's inside `docs/screenshots/` in the repo.

## 🔮 Future Improvements

- Hash user passwords (e.g., with BCrypt) instead of storing them as plain text
- Add unit tests for the Business Layer
- Migrate the Data Access Layer to use an ORM (e.g., Entity Framework) or Dapper
- Add a reporting module with exportable PDF/Excel reports
- Consider a web-based version (ASP.NET Core) for remote access

## 📄 License

This project is licensed under the [MIT License](LICENSE) — feel free to use, modify, and learn from it.

---

**Author:** [Galal Alzumaili](https://github.com/Galal-Alzumaili)
