# Gift of the Givers – Disaster Relief Management System
<img width="693" height="234" alt="image" src="https://github.com/user-attachments/assets/909f08a3-c3e8-4645-9ed1-46e71d47eb3d" />

An ASP.NET Core MVC application developed to support the Gift of the Givers Foundation's disaster relief activities. The system provides a central platform where donors can contribute towards relief projects, volunteers can submit applications, and employees can manage donations, projects and other organisational activities.

The application uses **C#, ASP.NET Core MVC (.NET 8), Entity Framework Core, SQL Server and ASP.NET Identity**.

## Project Overview

The system was developed to bring the main activities of a disaster relief organisation into one application.

Users can:
- Browse relief projects and view their funding progress.
- Make donations with or without creating an account.
- Register as volunteers.
- Access donation information and tax certificates.
- Manage their profiles and account settings.

Employees have access to a separate portal where they can manage relief projects, review donations, process volunteer applications and view reports.

## Technology Stack

| Technology | Purpose |
|---|---|
| C# | Main programming language |
| ASP.NET Core MVC (.NET 8) | Web application framework |
| Entity Framework Core | Database access and management |
| SQL Server / Azure SQL | Database |
| ASP.NET Core Identity | Authentication and role management |
| Razor Views | Frontend rendering |
| HTML, CSS and JavaScript | User interface |
| QuestPDF | Generating downloadable PDF tax certificates |
| Azure Functions | Tax certificate functionality |
| Azure DevOps | Work tracking, source control and build pipeline |

## Solution Structure

The solution is organised into separate areas to keep the application maintainable and make it easier to extend.

```text
GiftOfTheGivers.sln
│
├── GiftOfTheGivers.Web/
│   ├── Controllers/
│   │   ├── HomeController
│   │   ├── AccountController
│   │   ├── DonationController
│   │   ├── VolunteerController
│   │   ├── ProjectController
│   │   ├── EmployeeController
│   │   ├── NewsController
│   │   └── DonorController
│   │
│   ├── Models/
│   ├── ViewModels/
│   ├── Data/
│   ├── Repositories/
│   ├── Services/
│   ├── Views/
│   └── wwwroot/
│
├── GiftOfTheGivers.Functions/
│
├── GiftOfTheGivers.Helpers/
│
└── GiftOfTheGivers.Tests/
```

### Main application components

**Controllers**

Handle incoming requests, process user actions and coordinate communication between the views and application services.

**Models**

Represent the main entities in the system, including donors, employees, donations, relief projects, volunteers, news articles and tax certificates.

**ViewModels**

Provide the data required by individual pages and forms. They also contain validation rules to ensure that submitted information meets the application's requirements.

**Data**

Contains the Entity Framework Core database context and database initialisation logic.

**Repositories**

Provide a structured way of accessing and managing database records. The application uses a generic repository, entity-specific repositories and a Unit of Work to coordinate database operations.

**Services**

Contain the application's business logic, including donation processing, volunteer management, project management, reference number generation and tax certificate handling.

**Views**

Contain the Razor pages displayed to users.

**Helpers**

Provide shared functionality used by the application.

**Functions**

Contains the Azure Functions project, including the `GenerateTaxCertificate` HTTP-triggered function.

**Tests**

Contains automated tests used to check application functionality and integration between components.

## Application Architecture

The application follows the Model-View-Controller (MVC) architectural pattern, with additional repository and service layers.

- **MVC:** Separates application data, request handling and user interface responsibilities.
- **Repository pattern:** Provides a consistent approach to database operations.
- **Unit of Work:** Coordinates repository operations and saves changes through a shared database context.
- **Service layer:** Keeps business rules separate from controller logic.
- **Dependency Injection:** Registers and provides the required services, repositories, database context and Identity components through `Program.cs`.

This structure makes the application easier to maintain and allows individual components to be updated without unnecessarily affecting the rest of the system.

## Authentication and User Roles

Authentication is handled through ASP.NET Core Identity.

The application supports two main roles:

### Donor
Donors can:
- Register and sign in.
- Browse relief projects.
- Make donations.
- View their donation history.
- Access available tax certificates.
- Update their profile and account settings.

### Employee
Employees have access to the staff portal, where they can:
- View the employee dashboard.
- Manage relief projects.
- Review donation records.
- View and manage volunteer applications.
- Access project funding information and reports.
- Manage news content.

The application also supports anonymous donations. A visitor can make a donation without signing in, and the system creates a guest donor record using the details supplied on the donation form.

## Database

The application uses Entity Framework Core with a SQL Server database.

It supports SQL Server LocalDB for local development and Azure SQL Database through the configured connection string.

The database is managed using the Code-First approach. Entity Framework Core migrations are used to create and update the database structure.

The startup initialisation process in `SeedData.cs` is responsible for:
1. Applying pending database migrations.
2. Creating the Employee and Donor roles.
3. Adding six sample relief projects.
4. Adding three sample news articles.
5. Creating demonstration employee and donor accounts.

### Demo accounts

The following accounts are provided for local testing:

| Role | Email | Password |
|---|---|---|
| Employee | employee@giftofthegivers.org | Employee@2026! |
| Donor | donor@example.com | Donor@2026! |

**Security note:** These are development demonstration credentials. Change or remove them before using the application in a production environment. Never use demonstration passwords for real accounts.

## Reference Number Generation

The application uses a dedicated `ReferenceNumberService` to generate unique, sequential reference numbers.

The references include the record type, calendar year and a sequential number.

Examples:

| Record type | Example |
|---|---|
| Donation | `DON-2026-000001` |
| Volunteer application | `VOL-2026-000001` |
| Section 18A tax certificate | `GG18A-2026-000001` |

This makes it easier to identify and track individual records.

## Donation and Tax Certificates

The donation functionality allows visitors and registered donors to contribute towards relief projects.

Once a donation has been completed, the application uses its tax certificate functionality to issue the associated Section 18A certificate.

The `TaxCertificateService` handles certificate issuance, while QuestPDF is used to generate printable PDF documents.

Registered donors can access their available certificates through the donor portal and download them using the `DonorController`.

The solution also includes an Azure Functions project with an HTTP-triggered function named `GenerateTaxCertificate`. The web application communicates with this function as part of the donation process.

## Getting Started

### Requirements

Before running the application, make sure you have:

- Visual Studio 2022.
- .NET 8 SDK.
- ASP.NET and web development workload.
- SQL Server LocalDB or SQL Server.
- Git.
- Internet access to restore NuGet packages.

### 1. Clone the repository

```bash
git clone https://github.com/Phumu82/Gift-of-the--Givers.git
```

Move into the project directory:

```bash
cd Gift-of-the--Givers
```

### 2. Open the solution

Open `GiftOfTheGivers.sln` in Visual Studio 2022.

Allow Visual Studio to restore the required NuGet packages.

Alternatively, run:

```bash
dotnet restore
```

### 3. Configure the database

Open the application's `appsettings.json` file and check the `DefaultConnection` setting.

For local development, configure it to use your SQL Server LocalDB instance.

Do not commit passwords, production connection strings or other sensitive configuration values to the repository.

### 4. Apply database migrations

If the database has not yet been created, run the following commands from the `GiftOfTheGivers.Web` project directory:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

If the initial migration already exists in the repository, you only need to run:

```bash
dotnet ef database update
```

The application also uses its startup initialisation process to apply pending migrations and seed the database.

### 5. Run the application

From the solution directory:

```bash
dotnet run --project GiftOfTheGivers.Web
```

Alternatively, select the web project as the startup project in Visual Studio and press **F5**.

Once the application starts, open the local address displayed in the terminal or Visual Studio output window.

### 6. Run the Azure Functions project

The tax certificate function runs separately from the main web application during local development.

Start the `GiftOfTheGivers.Functions` project and ensure that the function URL matches the configuration in the web application's `appsettings.json`.

The function used by the application is:

```text
GenerateTaxCertificate
```

## Testing

The solution includes automated tests for different parts of the application.

The testing project uses:
- **xUnit** for automated testing.
- **Moq** for mocking dependencies.
- **Entity Framework Core InMemory** for integration testing.
- **Selenium WebDriver** for browser-based UI testing.
- **k6** for load and stress testing.

The project testing activities include:
- Unit tests for donation helpers, form validation and reference number generation.
- Integration tests for services, repositories and database operations.
- Selenium tests for user interface functionality.
- Load and stress tests to evaluate application performance.

To execute the automated test suite, run:

```bash
dotnet test GiftOfTheGivers.Tests/GiftOfTheGivers.Tests.csproj
```

Some UI tests require the website to be running before they can execute.

## Azure DevOps and Build Pipeline

The project uses Azure DevOps to manage development activities and automate the build and testing process.

The Azure DevOps environment includes:

- **Azure Boards:** Used to organise project work into Epics, Features, Product Backlog Items and Tasks.
- **Azure Repos:** Used to maintain the project's source code.
- **Azure Pipelines:** Used to restore dependencies, build the solution, execute automated tests and publish build artefacts.

The pipeline packages the web application, Azure Functions project and database script.

The deployment stage is configured separately and requires the appropriate Azure resources and service connection before it can be enabled.

## Azure Deployment Considerations

The application is designed to support deployment to Azure App Service, Azure Functions and Azure SQL Database.

Before production deployment:

1. Configure the production Azure SQL connection string.
2. Create and configure the required Azure App Service.
3. Create the Azure Function App.
4. Configure the Azure DevOps service connection.
5. Replace placeholder resource names in the pipeline.
6. Configure production application settings and secrets.
7. Enable and verify the deployment stage.
8. Test authentication, donations, database connectivity and tax certificate generation in the deployed environment.

Sensitive configuration should be stored using Azure App Service Configuration or Azure Key Vault rather than committed to source control.

The application uses SQL Server retry-on-failure configuration to help handle transient database connection issues.

## Future Improvements

Potential areas for further development include:

- Integrating a production-ready payment gateway.
- Improving donation transaction handling and failure recovery.
- Completing and verifying automated Azure deployment.
- Expanding automated test coverage.
- Improving monitoring and application logging.
- Strengthening production security and configuration management.
- Adding further reporting and analytics features.

## Project Repository

**GitHub:** https://github.com/Phumu82/Gift-of-the--Givers

**Azure DevOps:** https://dev.azure.com/ST10187312

## Licence and Third-Party Components

QuestPDF is used for PDF generation. Its applicable licence and usage conditions should be reviewed before production distribution.

Third-party libraries and packages remain subject to their respective licences.

---

**Gift of the Givers – Disaster Relief Management System**

Developed using ASP.NET Core MVC, C#, Entity Framework Core and Microsoft SQL Server.
