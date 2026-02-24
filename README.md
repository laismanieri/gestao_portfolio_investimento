# Investment Portfolio Management System

<p align="center">
  <img src="https://img.shields.io/badge/Status-Doing-green"/>
  <img src="https://img.shields.io/badge/Architecture-Clean%20Architecture-blue"/>
  <img src="https://img.shields.io/badge/Backend-ASP.NET%20Core-purple"/>
  <img src="https://img.shields.io/badge/Database-SQL%20%7C%20MySQL-orange"/>
</p>

---

# 📌 Overview

The Investment Portfolio Management System is an enterprise-level backend application designed for financial consulting companies to manage financial products, client investments, transactions, and automated maturity notifications.

The system follows **Clean Architecture principles**, ensuring clear separation of concerns, maintainability, scalability, and robustness.

---

# 🏗 Architectural Design

The project is structured using a layered architecture inspired by Clean Architecture:

## Layers

### 1️⃣ Presentation Layer (Controllers)
- Handles HTTP requests and responses
- Returns semantic HTTP status codes
- Does NOT contain business logic
- Uses Request/Response DTOs

### 2️⃣ Application Layer (Services)
- Contains business rules
- Orchestrates use cases
- Returns DTOs (never Entities)
- Handles validation and domain consistency

### 3️⃣ Domain Layer
- Core business entities
- BaseEntity with:
  - Internal numeric ID
  - Public GUID
  - CreatedAt / UpdatedAt (UTC)
- Business invariants

### 4️⃣ Infrastructure Layer
- Entity Framework Core
- Database persistence
- External integrations (SendGrid)
- Quartz scheduled jobs
- Logging configuration

---

# ✅ Architectural Improvements

## Separation of Responsibilities
- Controller → HTTP only
- Service → Business logic
- Repository → Persistence
- Mapper → Entity ↔ DTO conversion

## API Contract Clarity
- Request DTO ≠ Response DTO
- Internal database ID is NOT exposed
- GUID used externally
- Proper HTTP semantics:
  - 200 OK
  - 201 Created
  - 204 No Content
  - 400 Bad Request
  - 404 Not Found

## Domain Modeling
- BaseEntity abstraction
- Reduced coupling between JPA/EF and JSON
- Prevention of serialization loops
- Protection against LazyInitialization-like issues

---

# 🔐 Security

## Data Exposure Protection
- Internal numeric IDs are hidden
- Public GUID used for external identification
- DTO-based exposure strategy

## API Safety
- Controlled DELETE, PUT and GET operations
- No direct entity exposure
- Strict request validation

## Environment Security
- SendGrid API key stored as environment variable
- No secrets committed to repository

---

# 📊 Observability & Logging

Structured logging implemented to improve:

- Traceability
- Production debugging
- Audit capability
- Error tracking

Each critical action logs:

- Operation name
- Entity identifier (GUID)
- Timestamp
- Result (Success / Failure)

This significantly increases system robustness and maintainability.

---

# 📈 Scalability Considerations

- Layered separation enables horizontal scaling
- Business logic isolated from HTTP layer
- Database abstraction via EF Core
- Quartz scheduler decoupled from controllers
- DTO isolation prevents breaking API contracts

---

# 🛠 Technology Stack

- C#
- ASP.NET Core
- Entity Framework Core
- SQL Server / MySQL
- Quartz (Job scheduling)
- SendGrid (Email service)
- Swagger (API documentation)
- Git

---
 ### Prerequisites
  Before getting started, make sure you have the following tools installed on your machine:
  - **SDK .NET Core:** Ensure you have the .NET Core SDK installed on your machine. You can download it **[here](https://dotnet.microsoft.com/en-us/download)**
  - **Development Tool:** It is recommended to use a code editor such as [VSCode](https://code.visualstudio.com/) or [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/)
  - **Postman (Optional):** To test the APIs, you can use Postman. It can be downloaded **[here](https://www.postman.com/downloads/)**

---

## 🛠 Development Environment Setup:

  1. Install Visual Studio or Visual Studio Code.

  2. Clone the Repository:
      Clone the repository to your local development environment using the following command:
     ```sh
     git clone https://github.com/laismanieri/gestao_portfolio_investimento.git
     ```

  3. Navigate to the Project Directory:
      Navigate to the project directory using the terminal or command prompt:
     ```sh
     cd gestao_portfolio_investimento
     ```

  4. Restore Dependencies:
      Use the `dotnet restore` command to restore the project dependencies:
     ```sh
     dotnet restore
     ```

  5. Configure the Application:
      **SQL Server:** Configure the connection string in the `appsettings.json` file:
     ```sh
     "ConnectionStrings": {
          "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
      }
     ```
     **MySQL:** Configure the connection string in the `appsettings.json` file for MySQL if you are using Pomelo.EntityFrameworkCore.MySql:
     ```sh
     "ConnectionStrings": {
          "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
      }
     ```

  6. Run Database Migrations:
      Apply the migrations to set up the database:
     ```sh
     dotnet ef database update
     ```

  7. Run the Application:
      Apply the migrations to configure the database:
     ```sh
     dotnet ef database update
     ```

  8. Test the APIs:
      To test the APIs, access the usage documentation [here](https://github.com/laismanieri/gestao_portfolio_investimento/blob/main/GETTING_STARTED.md).

  9. Additional Documentation:
      If needed, refer to the official .NET Core documentation for more information on developing and running .NET Core applications: **[here](https://learn.microsoft.com/en-us/dotnet/fundamentals/)**

---

<div align="justify"> 



## Project Dependency Documentation

A dependency is an external component or library that a software project needs in order to work correctly. These dependencies provide additional functionality that is not included in the project's core code.
This document explains each dependency used in the project and how to install them via NuGet in Visual Studio Code.

## Dependências
### iTextSharp.LGPLv2.Core
- **Description:** Library for creating and manipulating PDF documents.
- **Installation:**
  ```bash
  dotnet add package iTextSharp.LGPLv2.Core --version 3.4.20

### Microsoft.EntityFrameworkCore
- **Description:** ORM (Object-Relational Mapping) data access provider for Entity Framework Core.
- **Installation:**
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore --version 6.0.29

### Microsoft.EntityFrameworkCore.Design
- **Description:** : Design-time tools for Entity Framework Core, including migration
- **Installation:**
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.29

### Microsoft.EntityFrameworkCore.SqlServer
- **Description:** SQL Server database provider for Entity Framework Core.
- **Installation:**
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.29

### Microsoft.EntityFrameworkCore.Tools
- **Description:** Entity Framework Core tools package (used for migrations and EF CLI tooling support).
- **Installation:**
  ```bash
  dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.29

### Microsoft.VisualStudio.Web.CodeGeneration.Design
- **Description:** Code generation (scaffolding) tools for ASP.NET Core.
- **Installation:**
  ```bash
  dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 6.0.16

### Pomelo.EntityFrameworkCore.MySql
- **Description:** MySQL database provider for Entity Framework Core.
- **Installation:**
  ```bash
  dotnet add package Pomelo.EntityFrameworkCore.MySql --version 6.0.2

### Quartz
- **Description:** Job scheduling library for background task automation.
- **Installation:**
  ```bash
  dotnet add package Quartz --version 3.9.0

### SendGrid
- **Description:** SendGrid API client used for sending transactional emails.
- **Installation:**
  ```bash
  dotnet add package SendGrid --version 9.29.3

### Swashbuckle.AspNetCore
- **Description:** Generates Swagger/OpenAPI documentation for ASP.NET Core APIs.
- **Installation:**
  ```bash
  dotnet add package Swashbuckle.AspNetCore --version 6.5.0

## NuGet Installation Instructions in Visual Studio Community

### Step by step

1. Open Visual Studio Community:  
   Launch Visual Studio Community and open your project.

2. NuGet Package Manager:  
   Right-click the solution or the project where you want to add dependencies.  
   Select **Manage NuGet Packages...** from the context menu.

3. Search for packages:  
   In the NuGet Package Manager, go to the **Browse** tab.  
   In the search box, type the name of the package you want to install (for example, `iTextSharp.LGPLv2.Core`).

4. Install packages:  
   Select the correct package from the search results list.  
   Click the **Install** button.

5. Accept the license terms (if prompted):  
   Repeat steps 3 and 4 for each dependency listed below.

---

  ## ⚙️ SendGrid Environment Variable Configuration
Step by Step
1. Create a SendGrid Account:
Go to the SendGrid website and create an account if you don't already have one.

2. Obtain your SendGrid API Key:
Log in to your SendGrid account.
In the SendGrid dashboard, navigate to API settings or API keys.
Create a new API key or copy an existing one.

3. Set the Environment Variable on your System:
In your application code, you can access this environment variable to retrieve the SendGrid API key.
Depending on the programming language and framework you are using, there may be different ways to access environment variables. However, most languages offer a simple and straightforward way to do this.

4. Test the Configuration:
After configuring the environment variable, test your application to ensure it is using the SendGrid API key correctly.
You can send test emails to verify that sending is working as expected.
   
---

  ## ⚙️ Features 
      
:heavy_check_mark: Client Registration: Allows adding new clients to the system. For each client, details such as name, email address, date of birth, and address are provided.

:heavy_check_mark: Investment Registration: Allows adding new investments to the system. Each investment is associated with a client and a specific financial product. Details such as quantity, purchase value, and maturity date are provided during registration.

:heavy_check_mark: Financial Product Registration: Allows adding new financial products to the system. Financial products represent the assets available for investment and may include stocks, bonds, mutual funds, etc. For each financial product, details such as type, name, value, etc. are provided.

:heavy_check_mark: Transaction Registration: Allows adding new transactions to the system. Transactions represent buy and sell operations of financial products. Each transaction is associated with a specific investment and includes details such as quantity, unit value, and transaction type (buy or sell).

:heavy_check_mark: Client Listing: Allows viewing a list of all clients registered in the system, including details such as name, email, date of birth, and address.

:heavy_check_mark: Investment Listing: Allows viewing a list of all investments registered in the system, including details such as the associated client, financial product, quantity, purchase value, and maturity date.

:heavy_check_mark: Financial Product Listing: Allows viewing a list of all financial products registered in the system, including details such as type, name, value, etc.

:heavy_check_mark: Transaction Listing: Allows viewing a list of all transactions registered in the system, including details such as the associated investment, quantity, unit value, and transaction type.

:heavy_check_mark: Client Update: Allows updating the details of an existing client in the system, such as name, email address, date of birth, etc.

:heavy_check_mark: Investment Update: Allows updating the details of an existing investment in the system, such as quantity, purchase value, maturity date, etc.

:heavy_check_mark: Financial Product Update: Allows updating the details of an existing financial product in the system, such as type, name, value, etc.

:heavy_check_mark: Client Deletion: Allows deleting an existing client from the system along with all their associated investments.

:heavy_check_mark: Investment Deletion: Allows deleting an existing investment from the system.

:heavy_check_mark: Financial Product Deletion: Allows deleting an existing financial product from the system.

:heavy_check_mark: Trade Financial Product (Buy and Sell): Allows clients to buy or sell financial products available in the system.

:heavy_check_mark: Product Statement: Allows viewing a detailed statement of a specific financial product, including all transactions associated with it, such as purchases, sales, etc.

:heavy_check_mark: Product Statement (PDF): Allows generating a detailed statement of a specific financial product in PDF format, including all transactions associated with it, such as purchases, sales, etc.

:heavy_check_mark: Investment Statement by Client (PDF): Allows generating a detailed statement of investments by client in PDF format, including all associated transactions such as purchases, sales, etc.

:heavy_check_mark: Automatic Email Dispatch: Daily automated email with a list of investments that are close to their maturity date.

👨‍💻 Contributors
</div>
Backend

 [<img src="https://avatars.githubusercontent.com/u/82177551?v=4" width=115><br><sub>Lais Manieri</sub>](https://github.com/laismanieri) 

  


