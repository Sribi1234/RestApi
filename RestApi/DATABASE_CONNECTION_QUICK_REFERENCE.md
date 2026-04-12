# SQL Server Connection & Database Service Quick Reference

## Connection String
```
Data Source=SRIBINKK26\SQLEXPRESS;Initial Catalog=Credit;Integrated Security=True;Trust Server Certificate=True
```

## Key Services & Classes

### IDatabaseService (Primary Interface)
**Location:** `RestApi/Services/DatabaseService.cs`

```csharp
public interface IDatabaseService
{
    CreditDbContext GetContext();
    Task<bool> TestConnectionAsync();
    string GetConnectionInfo();
}
```

**Usage in Controllers:**
```csharp
public MyController(IDatabaseService databaseService)
{
    _databaseService = databaseService;
}

public async Task<IActionResult> GetData()
{
    var context = _databaseService.GetContext();
    var members = await context.Members.ToListAsync();
    return Ok(members);
}
```

### CreditDbContext
**Location:** `RestApi/Data/CreditDbContext.cs`

All DbSets available:
- `People` - User management
- `Members` - Main member records
- `Payments` - Payment transactions
- `Statements` - Member statements
- `Charges` - Charge records
- `Categories` - Charge categories
- `Providers` - Service providers
- `Corporations` - Corporate entities
- `Regions` - Geographic regions

### Health Check Endpoints
**GET** `/api/health/status` - Full system health check
**GET** `/api/health/database` - Database connection info only

## Database Tables & Relationships

```
Region (RegionNo: PK)
  ├─ Members (MemberNo: PK) → RegionNo: FK
  ├─ Corporations (CorpNo: PK) → RegionNo: FK
  └─ Providers (ProviderNo: PK) → RegionNo: FK

Corporation (CorpNo: PK)
  └─ Members (MemberNo: PK) → CorpNo: FK (nullable)

Member (MemberNo: PK)
  ├─ Payments (PaymentNo: PK) → MemberNo: FK
  ├─ Statements (StatementNo: PK) → MemberNo: FK
  └─ Charges (ChargeNo: PK) → MemberNo: FK

Category (CategoryNo: PK)
  └─ Charges (ChargeNo: PK) → CategoryNo: FK

Provider (ProviderNo: PK)
  └─ Charges (ChargeNo: PK) → ProviderNo: FK

Person (Id: PK) - users table
```

## Common CRUD Operations

### Read
```csharp
// Get all members
var members = await context.Members.ToListAsync();

// Get specific member
var member = await context.Members.FindAsync(memberId);

// Get member with related data
var member = await context.Members
    .Include(m => m.Payments)
    .Include(m => m.Statements)
    .FirstOrDefaultAsync(m => m.MemberNo == memberId);

// Get payments for member
var payments = await context.Payments
    .Where(p => p.MemberNo == memberId)
    .ToListAsync();
```

### Create
```csharp
var newPayment = new Payment
{
    MemberNo = 1,
    PaymentDt = DateTime.Now,
    PaymentAmt = 500.00m,
    PaymentCode = "PAY001"
};

context.Payments.Add(newPayment);
await context.SaveChangesAsync();
```

### Update
```csharp
var payment = await context.Payments.FindAsync(paymentId);
if (payment != null)
{
    payment.PaymentAmt = 600.00m;
    await context.SaveChangesAsync();
}
```

### Delete
```csharp
var payment = await context.Payments.FindAsync(paymentId);
if (payment != null)
{
    context.Payments.Remove(payment);
    await context.SaveChangesAsync();
}
```

## Configuration Files

- **Development:** `appsettings.Development.json`
- **Production:** `appsettings.json`
- **DI Setup:** `Program.cs` (lines 26-31)

## Dependency Injection

Register in `Program.cs`:
```csharp
builder.Services.AddDbContext<CreditDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDatabaseService, DatabaseService>();
```

## Entity Constraints

- Foreign Key Delete Behavior: **Restrict** (prevents cascade deletes)
- All entities require primary keys
- Optional foreign keys: `CorpNo` in Member, `StatementNo` in Payment
- String fields: Allow null where marked with `?` in models

## Logging

Database operations are logged via:
- Serilog (configured in Program.cs)
- Log file location: `Data/myapp-[date].txt`
- Console output enabled

## Error Handling

Common issues and solutions:

| Error | Solution |
|-------|----------|
| "Invalid object name 'Members'" | Table missing in database |
| "The entity type 'X' requires a primary key" | Entity not configured in CreditDbContext |
| "Connection timeout" | Check SQL Server is running on SRIBINKK26\SQLEXPRESS |
| "Login failed" | Verify Windows user has database permissions |

## Test Connection

```csharp
var service = serviceProvider.GetRequiredService<IDatabaseService>();
var connected = await service.TestConnectionAsync();
```

Or use endpoint:
```
GET http://localhost:5000/api/health/status
```

## Server Details

- **Server:** SRIBINKK26\SQLEXPRESS
- **Database:** Credit
- **Authentication:** Windows Integrated Security
- **Port:** Default (1433)
- **Trust Certificate:** Yes

## Documentation

Full documentation available in: `RestApi/DATABASE_CONNECTION_SETUP.md`
