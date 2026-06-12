# HRM — Human Resource Management System

A .NET 10 REST API for managing core HR operations: employee records, organizational structure, attendance, leave, payroll, training, and authentication. The solution follows **Clean Architecture** with four projects and uses **Entity Framework Core 10** with **SQL Server** as the persistence layer.

---

## Solution Structure

| Project | Responsibility |
|---|---|
| **HR.API** | ASP.NET Core Web API entry point, OpenAPI/Scalar docs, Identity registration, CORS |
| **HR.Application** | Application layer (references Domain; business logic placeholder) |
| **HR.Domain** | Entities, enums, Fluent API configurations, repository interfaces, Unit of Work contract |
| **HR.Infrastructure** | `HRDbContext`, EF Core migrations, repository implementations, DI registration |

```
HRM/
├── HR.API/                 # Web API host
├── HR.Application/         # Application services
├── HR.Domain/              # Domain model & contracts
│   └── Data/
│       ├── Entities/       # EF entity classes
│       ├── Configuration/  # Fluent API (IEntityTypeConfiguration)
│       └── Entities/Enums/ # Domain enumerations
└── HR.Infrastructure/      # EF Core persistence
    ├── Persistance/        # HRDbContext
    ├── Migrations/         # Database migrations
    ├── Repository/         # Repository implementations
    └── UnitOfWork/         # Unit of Work implementation
```

