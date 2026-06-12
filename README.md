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

---

## Technology Stack

| Component | Technology |
|---|---|
| Runtime | .NET 10 |
| ORM | Entity Framework Core 10.0.8 |
| Database | Microsoft SQL Server |
| Authentication | ASP.NET Core Identity 10 |
| API Documentation | OpenAPI + Scalar |

---

## Database Overview

### Technology

The application uses **Microsoft SQL Server** accessed through **Entity Framework Core** with the `Microsoft.EntityFrameworkCore.SqlServer` provider. The database context is `HRDbContext`, which extends `IdentityDbContext<User, Role, string>` to combine HR domain tables with ASP.NET Identity tables in a single database.

### Structure

The schema is organized around **`Employees`** as the central entity. Supporting tables cover:

- **Organization** — departments (hierarchical), shifts, locations
- **Time & attendance** — daily attendance records tied to employee, shift, and location
- **Leave management** — leave requests, leave types, approvers
- **Compensation** — payroll runs and employee deductions
- **Learning & development** — trainings and employee enrollment (many-to-many)
- **Security** — ASP.NET Identity tables linked to employees

### Purpose

The database stores the full lifecycle of workforce data: who employees are, where they work, how they are organized, when they attend, what leave they take, how they are paid, what training they complete, and which system accounts they use to authenticate.

### Configuration

- Connection string key: `ConnectionStrings:DefaultConnection` (defined in `HR.API/appsettings.Development.json`)
- Fluent API configurations are applied automatically from the `HR.Domain` assembly via `ApplyConfigurationsFromAssembly`
- Enum properties are stored as **strings** in the database (not integer values)
- Integer primary keys use SQL Server **IDENTITY** columns

---

## Entities Documentation

All HR domain entities (except junction and Identity types) inherit from `BaseEntity`, which provides a single `int Id` primary key.

### BaseEntity

| Property | Type | Required | Default | Notes |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity (auto-increment) | Primary key |

---

### Employee (`Employees`)

**Purpose:** Core workforce record. Stores personal, employment, compensation, and organizational assignment data. Acts as the hub for attendance, leave, payroll, deductions, training, and login accounts.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `FirstName` | `nvarchar(50)` | Yes | — | Max 50 |
| `LastName` | `nvarchar(50)` | Yes | — | Max 50 |
| `DateOfBirth` | `date` | Yes | — | |
| `Gender` | `nvarchar(20)` | Yes | — | Enum: `Male`, `Female`, `Other` |
| `PhoneNumber` | `nvarchar(20)` | Yes | — | Max 20 |
| `NationalId` | `nvarchar(20)` | Yes | — | **Unique index** |
| `EmploymentType` | `nvarchar(30)` | Yes | — | Enum: `FullTime`, `PartTime`, `Contract` |
| `EmploymentStatus` | `nvarchar(30)` | Yes | — | Enum: `Active`, `OnLeave`, `Terminated` |
| `ProbationEndDate` | `datetime2` | No | `null` | |
| `BaseSalary` | `decimal(18,2)` | Yes | — | |
| `HourlyRate` | `decimal(18,2)` | Yes | — | |
| `Position` | `nvarchar(100)` | Yes | — | Max 100 |
| `HireDate` | `date` | Yes | — | |
| `Nationality` | `nvarchar(50)` | Yes | — | Max 50 |
| `DepartmentId` | `int` | Yes | — | FK → `Departments.Id` (Restrict) |
| `ManagerId` | `int` | No | `null` | FK → `Employees.Id` (self-reference, Restrict) |
| `DefaultShiftId` | `int` | Yes | — | FK → `Shifts.Id` (Cascade) |

**Primary key:** `Id`

**Foreign keys:**

| Column | References | Delete behavior |
|---|---|---|
| `DepartmentId` | `Departments.Id` | Restrict |
| `ManagerId` | `Employees.Id` | Restrict |
| `DefaultShiftId` | `Shifts.Id` | Cascade |

**Navigation properties:** `Department`, `Manager`, `Subordinates`, `ManagedDepartment`, `Attendances`, `Leaves`, `ApprovedLeaves`, `Payrolls`, `EmployeeTrainings`, `EmployeeDeductions`, `DefaultShift`

---

### Department (`Departments`)

**Purpose:** Organizational unit with cost-center tracking, optional parent department (hierarchy), assigned manager, and employee membership.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `Name` | `nvarchar(100)` | Yes | — | **Unique index** |
| `CostCenter` | `nvarchar(20)` | Yes | — | Max 20 |
| `HeadCount` | `int` | Yes | `0` | |
| `ParentDepartmentId` | `int` | No | `null` | FK → `Departments.Id` (Restrict) |
| `ManagerId` | `int` | No | `null` | FK → `Employees.Id` (SetNull) |

**Primary key:** `Id`

**Foreign keys:**

| Column | References | Delete behavior |
|---|---|---|
| `ParentDepartmentId` | `Departments.Id` | Restrict |
| `ManagerId` | `Employees.Id` | SetNull |

**Important constraints:**

- `Name` is unique across all departments
- `ManagerId` has a **unique filtered index** (`[ManagerId] IS NOT NULL`) — each employee can manage at most one department

**Navigation properties:** `ParentDepartment`, `SubDepartments`, `Employees`, `Manager`

---

### Shift (`Shifts`)

**Purpose:** Defines work schedules. Supports fixed-time shifts (`StartTime`/`EndTime`) and flexible shifts (`IsFlexible`, `RequiredHours`). Used as an employee's default schedule and recorded on each attendance entry.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `Name` | `nvarchar(50)` | Yes | — | Max 50 |
| `IsFlexible` | `bit` | Yes | `false` | |
| `RequiredHours` | `int` | No | `null` | For flexible shifts |
| `StartTime` | `time` | Yes | — | |
| `EndTime` | `time` | Yes | — | |
| `GracePeriodMinutes` | `int` | Yes | `0` | Late-arrival tolerance |

**Primary key:** `Id`

**Navigation properties:** `Attendances`, `Employees` (default shift)

---

### Location (`Locations`)

**Purpose:** Physical or remote work locations used to record where attendance was captured (office, remote, GPS coordinates).

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `IsRemote` | `bit` | Yes | `false` | |
| `Address` | `nvarchar(500)` | No | `null` | Max 500 |
| `Lat` | `decimal(10,7)` | No | `null` | Latitude |
| `Long` | `decimal(10,7)` | No | `null` | Longitude |

**Primary key:** `Id`

**Navigation properties:** `Attendances`

---

### Attendance (`Attendances`)

**Purpose:** Daily attendance record per employee, including clock-in/out times, status, source, and calculated lateness/overtime metrics.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `Date` | `date` | Yes | — | |
| `TimeIn` | `datetime2` | No | `null` | |
| `TimeOut` | `datetime2` | No | `null` | |
| `Status` | `nvarchar(20)` | Yes | — | Enum: `Present`, `Absent`, `Late`, `HalfDay` |
| `Source` | `nvarchar(30)` | Yes | — | Enum: `Biometric`, `App`, `Manual` |
| `LateMinutes` | `int` | Yes | `0` | |
| `EarlyDepartureMinutes` | `int` | Yes | `0` | |
| `OverTimeHours` | `int` | Yes | `0` | |
| `EmployeeId` | `int` | Yes | — | FK → `Employees.Id` (Cascade) |
| `LocationId` | `int` | Yes | — | FK → `Locations.Id` (Restrict) |
| `ShiftId` | `int` | Yes | — | FK → `Shifts.Id` (Restrict) |

**Primary key:** `Id`

**Foreign keys:**

| Column | References | Delete behavior |
|---|---|---|
| `EmployeeId` | `Employees.Id` | Cascade |
| `LocationId` | `Locations.Id` | Restrict |
| `ShiftId` | `Shifts.Id` | Restrict |

**Important constraints:**

- **Unique composite index** on (`EmployeeId`, `Date`) — one attendance record per employee per day

---

### LeaveType (`LeaveTypes`)

**Purpose:** Catalog of leave categories (e.g., annual, sick) with policy rules for paid status, carry-over, approval requirements, and annual limits.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `Name` | `nvarchar(50)` | Yes | — | **Unique index** |
| `MaxDaysPerYear` | `int` | Yes | `0` | |
| `IsPaid` | `bit` | Yes | `false` | |
| `CarryOverAllowed` | `bit` | Yes | `false` | |
| `RequiresApproval` | `bit` | Yes | `false` | |

**Primary key:** `Id`

**Navigation properties:** `Leaves`

---

### Leave (`Leaves`)

**Purpose:** Leave request submitted by an employee, referencing a leave type, tracking approval workflow and status.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `StartDate` | `date` | Yes | — | |
| `EndDate` | `date` | Yes | — | |
| `Status` | `nvarchar(20)` | Yes | — | Enum: `Pending`, `Approved`, `Rejected` |
| `ApprovedAt` | `date` | No | `null` | |
| `EmployeeId` | `int` | Yes | — | FK → `Employees.Id` (Cascade) |
| `ApproverId` | `int` | Yes | — | FK → `Employees.Id` (Restrict) |
| `LeaveTypeId` | `int` | Yes | — | FK → `LeaveTypes.Id` (Restrict) |

**Primary key:** `Id`

**Foreign keys:**

| Column | References | Delete behavior |
|---|---|---|
| `EmployeeId` | `Employees.Id` | Cascade |
| `ApproverId` | `Employees.Id` | Restrict |
| `LeaveTypeId` | `LeaveTypes.Id` | Restrict |

**Navigation properties:** `Employee`, `Approver`, `LeaveType`

---

### Payroll (`Payrolls`)

**Purpose:** Payroll record for an employee over a defined pay period, tracking gross pay, deductions, net pay, currency, and payment status.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `GrossPay` | `decimal(18,2)` | Yes | — | |
| `TotalDeductions` | `decimal(18,2)` | Yes | `0.00` | |
| `NetPay` | `decimal(18,2)` | Yes | — | |
| `PayPeriodStart` | `date` | Yes | — | |
| `PayPeriodEnd` | `date` | Yes | — | |
| `PaymentStatus` | `nvarchar(20)` | Yes | — | Enum: `Drafted`, `Processed`, `Paid` |
| `Currency` | `nvarchar(3)` | Yes | — | ISO 4217 code (e.g., USD) |
| `EmployeeId` | `int` | Yes | — | FK → `Employees.Id` (Cascade) |

**Primary key:** `Id`

**Foreign keys:**

| Column | References | Delete behavior |
|---|---|---|
| `EmployeeId` | `Employees.Id` | Cascade |

**Important constraints:**

- **Unique composite index** on (`EmployeeId`, `PayPeriodStart`, `PayPeriodEnd`) — one payroll per employee per pay period

**Navigation properties:** `Employee`

---

### EmployeeDeductions (`EmployeeDeductions`)

**Purpose:** Tracks deductions applied to an employee (by day or hour), including calculated monetary amount, reason, and whether the deduction has been applied to a payroll run.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `ActionDate` | `datetime2` | Yes | — | When deduction occurred |
| `Unit` | `nvarchar(30)` | Yes | — | Enum: `Day`, `Hour` |
| `Quantity` | `decimal(18,2)` | Yes | — | Units deducted |
| `CalculatedAmount` | `decimal(18,2)` | Yes | — | Monetary value |
| `Reason` | `nvarchar(500)` | Yes | — | Max 500 |
| `IsAppliedToPayroll` | `bit` | Yes | `false` | |
| `EmployeeId` | `int` | Yes | — | FK → `Employees.Id` (Cascade) |

**Primary key:** `Id`

**Foreign keys:**

| Column | References | Delete behavior |
|---|---|---|
| `EmployeeId` | `Employees.Id` | Cascade |

**Navigation properties:** `Employee`

---

### Training (`Trainings`)

**Purpose:** Training course or session catalog entry with title, description, scheduled date, and duration.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `Id` | `int` | Yes | Identity | PK |
| `Title` | `nvarchar(200)` | Yes | — | Max 200 |
| `Description` | `nvarchar(1000)` | No | `null` | Max 1000 |
| `Date` | `date` | Yes | — | |
| `DurationInHours` | `int` | Yes | `0` | |

**Primary key:** `Id`

**Navigation properties:** `EmployeeTrainings`

---

### EmployeeTrainings (`EmployeeTrainings`)

**Purpose:** Junction table linking employees to trainings (many-to-many), tracking enrollment completion status and score.

| Property | Type | Required | Default | Constraints |
|---|---|---|---|---|
| `EmployeeId` | `int` | Yes | — | PK (composite), FK → `Employees.Id` (Cascade) |
| `TrainingId` | `int` | Yes | — | PK (composite), FK → `Trainings.Id` (Cascade) |
| `CompletionStatus` | `nvarchar(30)` | Yes | — | Enum: `Enrolled`, `InProgress`, `Completed`, `Failed` |
| `Score` | `int` | Yes | `0` | |

**Primary key:** Composite (`EmployeeId`, `TrainingId`)

**Foreign keys:**

| Column | References | Delete behavior |
|---|---|---|
| `EmployeeId` | `Employees.Id` | Cascade |
| `TrainingId` | `Trainings.Id` | Cascade |

**Navigation properties:** `Employee`, `Training`

---

## Identity Tables (ASP.NET Core Identity)

Authentication is provided by **ASP.NET Core Identity**, with custom `User` and `Role` types stored alongside HR data.

### User (`AspNetUsers`)

**Purpose:** Application login account linked to an `Employee` record. Extends `IdentityUser`.

| Property | Type | Required | Notes |
|---|---|---|---|
| `Id` | `nvarchar(450)` | Yes | PK (string GUID) |
| `EmployeeId` | `int` | Yes | FK → `Employees.Id` (Cascade) |
| `UserName` | `nvarchar(256)` | No | Unique when normalized |
| `NormalizedUserName` | `nvarchar(256)` | No | Unique index (`UserNameIndex`) |
| `Email` | `nvarchar(256)` | No | |
| `NormalizedEmail` | `nvarchar(256)` | No | Index (`EmailIndex`) |
| `EmailConfirmed` | `bit` | Yes | |
| `PasswordHash` | `nvarchar(max)` | No | |
| `SecurityStamp` | `nvarchar(max)` | No | |
| `ConcurrencyStamp` | `nvarchar(max)` | No | Concurrency token |
| `PhoneNumber` | `nvarchar(max)` | No | Identity phone (separate from employee phone) |
| `PhoneNumberConfirmed` | `bit` | Yes | |
| `TwoFactorEnabled` | `bit` | Yes | |
| `LockoutEnd` | `datetimeoffset` | No | |
| `LockoutEnabled` | `bit` | Yes | |
| `AccessFailedCount` | `int` | Yes | |

**Navigation properties:** `Employee`

**Identity configuration** (in `IdentityService`):

- Unique email required
- Minimum password length: 6
- No complexity requirements (digits, uppercase, etc. disabled)
- Email/phone confirmation not required for sign-in

### Role (`AspNetRoles`)

**Purpose:** Named security role for authorization. Extends `IdentityRole`.

| Property | Type | Required | Notes |
|---|---|---|---|
| `Id` | `nvarchar(450)` | Yes | PK |
| `Name` | `nvarchar(256)` | No | |
| `NormalizedName` | `nvarchar(256)` | No | Unique index (`RoleNameIndex`) |
| `ConcurrencyStamp` | `nvarchar(max)` | No | |

### Supporting Identity Tables

| Table | Purpose | Primary Key |
|---|---|---|
| `AspNetUserRoles` | User-to-role assignments | (`UserId`, `RoleId`) |
| `AspNetUserClaims` | Additional claims on users | `Id` (identity) |
| `AspNetRoleClaims` | Additional claims on roles | `Id` (identity) |
| `AspNetUserLogins` | External login providers | (`LoginProvider`, `ProviderKey`) |
| `AspNetUserTokens` | Authentication/reset tokens | (`UserId`, `LoginProvider`, `Name`) |

---

## Entity Relationships

### One-to-One

#### Employee ↔ Department (Manager)

| Aspect | Detail |
|---|---|
| Entities | `Employee` ↔ `Department` |
| Cardinality | One employee manages **at most one** department; one department has **at most one** manager |
| Foreign key | `Departments.ManagerId` → `Employees.Id` |
| Delete behavior | SetNull — deleting the manager employee clears `ManagerId` |
| Why | Models departmental leadership without requiring every department to have a manager |

---

### One-to-Many

#### Department → Employee

| Aspect | Detail |
|---|---|
| Cardinality | One department has many employees; each employee belongs to one department |
| Foreign key | `Employees.DepartmentId` → `Departments.Id` |
| Delete behavior | Restrict — cannot delete a department that still has employees |
| Why | Enforces organizational assignment and prevents orphaned employees |

#### Department → Department (Hierarchy)

| Aspect | Detail |
|---|---|
| Cardinality | One parent department has many sub-departments; each sub-department has one optional parent |
| Foreign key | `Departments.ParentDepartmentId` → `Departments.Id` |
| Delete behavior | Restrict |
| Why | Supports multi-level org charts (e.g., Engineering → Backend Team) |

#### Employee → Employee (Reporting Line)

| Aspect | Detail |
|---|---|
| Cardinality | One manager has many subordinates; each employee has one optional manager |
| Foreign key | `Employees.ManagerId` → `Employees.Id` |
| Delete behavior | Restrict |
| Why | Models direct reporting relationships independent of department structure |

#### Shift → Employee (Default Shift)

| Aspect | Detail |
|---|---|
| Cardinality | One shift is the default for many employees; each employee has one default shift |
| Foreign key | `Employees.DefaultShiftId` → `Shifts.Id` |
| Delete behavior | Cascade — deleting a shift removes employees referencing it as default |
| Why | Every employee has a baseline work schedule for attendance calculations |

#### Shift → Attendance

| Aspect | Detail |
|---|---|
| Cardinality | One shift applies to many attendance records; each attendance has one shift |
| Foreign key | `Attendances.ShiftId` → `Shifts.Id` |
| Delete behavior | Restrict |
| Why | Records which schedule was active on a given attendance day (may differ from default) |

#### Location → Attendance

| Aspect | Detail |
|---|---|
| Cardinality | One location has many attendance records; each attendance has one location |
| Foreign key | `Attendances.LocationId` → `Locations.Id` |
| Delete behavior | Restrict |
| Why | Tracks where attendance was recorded (office, remote, etc.) |

#### Employee → Attendance

| Aspect | Detail |
|---|---|
| Cardinality | One employee has many attendance records |
| Foreign key | `Attendances.EmployeeId` → `Employees.Id` |
| Delete behavior | Cascade |
| Why | Attendance is owned by the employee; deleted with the employee |

#### Employee → Leave (Requester)

| Aspect | Detail |
|---|---|
| Cardinality | One employee submits many leave requests |
| Foreign key | `Leaves.EmployeeId` → `Employees.Id` |
| Delete behavior | Cascade |
| Why | Leave requests belong to the requesting employee |

#### Employee → Leave (Approver)

| Aspect | Detail |
|---|---|
| Cardinality | One employee (manager/HR) approves many leave requests |
| Foreign key | `Leaves.ApproverId` → `Employees.Id` |
| Delete behavior | Restrict |
| Why | Prevents deleting an approver who still has associated approval records |

#### LeaveType → Leave

| Aspect | Detail |
|---|---|
| Cardinality | One leave type categorizes many leave requests |
| Foreign key | `Leaves.LeaveTypeId` → `LeaveTypes.Id` |
| Delete behavior | Restrict |
| Why | Leave types are reference data; cannot be removed while in use |

#### Employee → Payroll

| Aspect | Detail |
|---|---|
| Cardinality | One employee has many payroll records (one per pay period) |
| Foreign key | `Payrolls.EmployeeId` → `Employees.Id` |
| Delete behavior | Cascade |
| Why | Payroll history is tied to the employee lifecycle |

#### Employee → EmployeeDeductions

| Aspect | Detail |
|---|---|
| Cardinality | One employee has many deduction entries |
| Foreign key | `EmployeeDeductions.EmployeeId` → `Employees.Id` |
| Delete behavior | Cascade |
| Why | Deductions accumulate against an employee and feed into payroll |

#### Employee → User (Identity)

| Aspect | Detail |
|---|---|
| Cardinality | One employee may have many login accounts; each user account links to one employee |
| Foreign key | `AspNetUsers.EmployeeId` → `Employees.Id` |
| Delete behavior | Cascade |
| Why | System access is bound to a real employee record; accounts are removed when the employee is deleted |

#### Identity Standard Relationships

- **User → UserRoles → Role** — many-to-many via `AspNetUserRoles`
- **User → UserClaims, UserLogins, UserTokens** — one-to-many (Cascade)
- **Role → RoleClaims** — one-to-many (Cascade)

---

### Many-to-Many

#### Employee ↔ Training (via `EmployeeTrainings`)

| Aspect | Detail |
|---|---|
| Entities | `Employee` ↔ `Training` through junction table `EmployeeTrainings` |
| Cardinality | Many employees enroll in many trainings |
| Foreign keys | `EmployeeTrainings.EmployeeId` → `Employees.Id`, `EmployeeTrainings.TrainingId` → `Trainings.Id` |
| Primary key | Composite (`EmployeeId`, `TrainingId`) |
| Delete behavior | Cascade on both sides |
| Payload columns | `CompletionStatus`, `Score` |
| Why | Tracks training enrollment and outcomes without duplicating employee or training data |

#### User ↔ Role (via `AspNetUserRoles`)

| Aspect | Detail |
|---|---|
| Cardinality | Many users can hold many roles |
| Foreign keys | `UserId` → `AspNetUsers.Id`, `RoleId` → `AspNetRoles.Id` |
| Why | Standard ASP.NET Identity role-based authorization |

---

## Database Schema Explanation

The schema radiates from **`Employees`** as the central hub:

```
                    ┌─────────────┐
                    │  LeaveTypes │
                    └──────┬──────┘
                           │
┌──────────┐    ┌──────────┴───┐    ┌────────────┐
│ Shifts   │◄───│   Employees   │───►│ Departments│
└────┬─────┘    └───────┬───────┘    └─────┬──────┘
     │                  │                   │ (self-ref hierarchy)
     │                  │                   │
     ▼                  ├──► Attendances ◄──┤
┌──────────┐           │         ▲          │
│Locations │───────────┘         │          │
└──────────┘                     │          │
                                 ├──► Leaves (requester + approver)
                                 ├──► Payrolls
                                 ├──► EmployeeDeductions
                                 ├──► EmployeeTrainings ◄──► Trainings
                                 └──► AspNetUsers (Identity)
```

**Organizational cluster:** `Departments` form a tree via `ParentDepartmentId`. Employees are assigned to exactly one department. Departments optionally designate one employee as manager (one-to-one). Employees also form a separate reporting hierarchy via `ManagerId`.

**Time cluster:** Each employee has a `DefaultShiftId`. Daily `Attendances` capture the actual shift worked, the location, clock times, and derived metrics. The unique (`EmployeeId`, `Date`) constraint enforces one record per day.

**Leave cluster:** `LeaveTypes` define policies. `Leaves` connect a requesting employee, an approving employee, and a leave type.

**Compensation cluster:** `Payrolls` store period-based pay summaries. `EmployeeDeductions` track individual deduction events that can be rolled into payroll (`IsAppliedToPayroll`).

**Training cluster:** `EmployeeTrainings` is the associative entity between `Employees` and `Trainings`.

**Security cluster:** `AspNetUsers` links Identity accounts to `Employees`. Standard Identity junction and claim tables provide roles and extended authorization.

### Delete Behavior Summary

| Behavior | Used when |
|---|---|
| **Cascade** | Child records are owned by parent (attendance, leaves, payroll, deductions, user accounts, default shift) |
| **Restrict** | Reference/lookup data or cross-entity references must be explicitly handled (department, location, shift on attendance, approver, leave type) |
| **SetNull** | Optional relationship where parent removal should not cascade (department manager) |

---

## Entity Relationship Diagram (Mermaid)

```mermaid
erDiagram
    Employees ||--o{ Attendances : "has"
    Employees ||--o{ Leaves : "requests"
    Employees ||--o{ Leaves : "approves"
    Employees ||--o{ Payrolls : "receives"
    Employees ||--o{ EmployeeDeductions : "has"
    Employees ||--o{ EmployeeTrainings : "enrolled_in"
    Employees }o--|| Departments : "belongs_to"
    Employees }o--o| Employees : "reports_to"
    Employees ||--o| Departments : "manages"
    Employees }o--|| Shifts : "default_shift"
    Employees ||--o{ AspNetUsers : "has_account"

    Departments ||--o{ Employees : "contains"
    Departments }o--o| Departments : "parent_of"

    Shifts ||--o{ Attendances : "scheduled_on"
    Shifts ||--o{ Employees : "assigned_as_default"

    Locations ||--o{ Attendances : "recorded_at"

    LeaveTypes ||--o{ Leaves : "categorizes"

    Trainings ||--o{ EmployeeTrainings : "includes"

    AspNetUsers }o--o{ AspNetRoles : "assigned_via_AspNetUserRoles"

    Employees {
        int Id PK
        string FirstName
        string LastName
        string NationalId UK
        int DepartmentId FK
        int ManagerId FK
        int DefaultShiftId FK
    }

    Departments {
        int Id PK
        string Name UK
        int ParentDepartmentId FK
        int ManagerId FK_UK
    }

    Attendances {
        int Id PK
        int EmployeeId FK
        int ShiftId FK
        int LocationId FK
        date Date
    }

    Leaves {
        int Id PK
        int EmployeeId FK
        int ApproverId FK
        int LeaveTypeId FK
    }

    Payrolls {
        int Id PK
        int EmployeeId FK
        date PayPeriodStart
        date PayPeriodEnd
    }

    EmployeeTrainings {
        int EmployeeId PK_FK
        int TrainingId PK_FK
        string CompletionStatus
    }

    EmployeeDeductions {
        int Id PK
        int EmployeeId FK
        decimal CalculatedAmount
    }

    Shifts {
        int Id PK
        string Name
        bool IsFlexible
    }

    Locations {
        int Id PK
        bool IsRemote
    }

    LeaveTypes {
        int Id PK
        string Name UK
    }

    Trainings {
        int Id PK
        string Title
    }

    AspNetUsers {
        string Id PK
        int EmployeeId FK
        string UserName
        string Email
    }

    AspNetRoles {
        string Id PK
        string Name
    }
```

---

## Data Access Layer

### DbContext

`HRDbContext` (`HR.Infrastructure/Persistance/HRDbContext.cs`) extends `IdentityDbContext<User, Role, string>` and exposes these `DbSet` properties:

| DbSet | Entity |
|---|---|
| `Employees` | `Employee` |
| `Departments` | `Department` |
| `Attendances` | `Attendance` |
| `Leaves` | `Leave` |
| `LeaveTypes` | `LeaveType` |
| `Trainings` | `Training` |
| `Payrolls` | `Payroll` |
| `EmployeeTrainings` | `EmployeeTrainings` |
| `Shifts` | `Shift` |
| `Locations` | `Location` |
| `EmployeeDeductions` | `EmployeeDeductions` |

Identity entities (`User`, `Role`) are managed through the base `IdentityDbContext` and ASP.NET Identity stores.

### Fluent API Configurations

All entity configurations implement `IEntityTypeConfiguration<T>` in `HR.Domain/Data/Configuration/`:

| Configuration class | Entity |
|---|---|
| `EmployeeConfiguration` | `Employee` |
| `DepartmentConfiguration` | `Department` |
| `AttendanceConfiguration` | `Attendance` |
| `LeaveConfiguration` | `Leave` |
| `LeaveTypeConfiguration` | `LeaveType` |
| `PayrollConfiguration` | `Payroll` |
| `TrainingConfiguration` | `Training` |
| `EmployeeTrainingsConfiguration` | `EmployeeTrainings` |
| `ShiftConfiguration` | `Shift` |
| `LocationConfiguration` | `Location` |
| `EmployeeDeductionsConfiguration` | `EmployeeDeductions` |

Configurations are auto-discovered and applied in `OnModelCreating` via `ApplyConfigurationsFromAssembly`.

### Repository Pattern

A generic repository provides standard CRUD operations:

```csharp
// IGenericRepository<T>
GetAllAsync(), GetByIdAsync(int id), FindAsync(predicate),
AddAsync(T), UpdateAsync(T), DeleteAsync(T)
```

Entity-specific repositories extend the generic interface without adding custom methods (thin wrappers):

| Interface | Implementation | Entity |
|---|---|---|
| `IEmployeeRepository` | `EmployeeRepository` | `Employee` |
| `IDepartmentRepository` | `DepartmentRepository` | `Department` |
| `IAttendanceRepository` | `AttendanceRepository` | `Attendance` |
| `ILeaveRepository` | `LeaveRepository` | `Leave` |
| `ILeaveTypeRepository` | `LeaveTypeRepository` | `LeaveType` |
| `ILocationRepository` | `LocationRepository` | `Location` |
| `IPayrollRepository` | `PayrollRepository` | `Payroll` |
| `IShiftRepository` | `ShiftRepository` | `Shift` |
| `ITrainingRepository` | `TrainingRepository` | `Training` |
| `IEmployeeTrainingsRepository` | `EmployeeTrainingsRepository` | `EmployeeTrainings` |
| `IEmployeeDeductionsRepository` | `EmployeeDeductionsRepository` | `EmployeeDeductions` |

All repositories are registered as **scoped** services in `InfrastructureServices.AddInfrastructure`.

### Unit of Work

`IUnitOfWork` / `UnitOfWork` provides `SaveChangesAsync()` to commit transactions. Currently exposes `IAttendanceRepository` and `IDepartmentRepository` as public properties; other repositories are injected independently.

---

## Migrations History

Migrations are stored in `HR.Infrastructure/Migrations/` and applied against SQL Server.

| Migration | Date | Description |
|---|---|---|
| `20260601124331_IntialCreate` | 2026-06-01 | Initial schema: employees, departments, attendances, leaves, trainings, payroll |
| `20260604103207_ERD Enhance` | 2026-06-04 | ERD refinements: relationships, constraints, additional columns |
| `20260606084003_Shift Entity Added` | 2026-06-06 | Added `Shifts` table; linked to attendances |
| `20260606120334_Salary and Payroll Enhance` | 2026-06-06 | Enhanced salary fields and payroll structure |
| `20260606132059_EmployeeDeductions Table` | 2026-06-06 | Added `EmployeeDeductions` table |
| `20260607125826_Flexible Shift + Default Employee Shift` | 2026-06-07 | Flexible shift support; `DefaultShiftId` on employees |
| `20260607164201_Identity` | 2026-06-07 | ASP.NET Identity tables; `EmployeeId` on users |

### Apply Migrations

```bash
cd HR.Infrastructure
dotnet ef database update --startup-project ../HR.API
```

### Add a New Migration

```bash
cd HR.Infrastructure
dotnet ef migrations add <MigrationName> --startup-project ../HR.API
```

---

## Getting Started

### Prerequisites

- .NET 10 SDK
- SQL Server (LocalDB or Express)
- Entity Framework Core CLI tools (`dotnet tool install --global dotnet-ef`)

### Connection String

Configure in `HR.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<your-server>;Database=HRM;Trusted_Connection=true;TrustServerCertificate=true"
  }
}
```

### Run the API

```bash
dotnet run --project HR.API
```

In Development, OpenAPI and Scalar API reference are available:

- OpenAPI document: `/openapi/v1.json`
- Scalar UI: `/scalar/v1`

CORS is configured to allow `http://localhost:4200` (Angular frontend).

---

## Domain Enumerations Reference

| Enum | Values | Used on |
|---|---|---|
| `Gender` | Male, Female, Other | `Employee` |
| `EmploymentType` | FullTime, PartTime, Contract | `Employee` |
| `EmploymentStatus` | Active, OnLeave, Terminated | `Employee` |
| `AttendanceStatus` | Present, Absent, Late, HalfDay | `Attendance` |
| `AttendanceSource` | Biometric, App, Manual | `Attendance` |
| `LeaveStatus` | Pending, Approved, Rejected | `Leave` |
| `PaymentStatus` | Drafted, Processed, Paid | `Payroll` |
| `CompletionStatus` | Enrolled, InProgress, Completed, Failed | `EmployeeTrainings` |
| `DeductionUnit` | Day, Hour | `EmployeeDeductions` |

All enums are persisted as **string values** in the database via EF Core value conversion.
