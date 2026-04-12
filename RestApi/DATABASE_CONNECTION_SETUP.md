# Database Connection Setup Documentation

## Connection String Configuration

**Server:** SRIBINKK26\SQLEXPRESS  
**Database:** Credit  
**Authentication:** Windows Integrated Security  
**Location:** `appsettings.Development.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=SRIBINKK26\\SQLEXPRESS;Initial Catalog=Credit;Integrated Security=True;Trust Server Certificate=True"
  }
}
```

## Architecture Overview

### Components

1. **CreditDbContext** (`Data/CreditDbContext.cs`)
   - Entity Framework Core DbContext
   - Configured for SQL Server
   - All entity mappings and relationships defined
   - Manages database operations

2. **DatabaseService** (`Services/DatabaseService.cs`)
   - Centralized database service
   - Connection testing
   - Configuration management
   - DI-friendly interface

3. **HealthController** (`Controllers/HealthController.cs`)
   - Health check endpoint
   - Database connectivity verification
   - Application status monitoring

4. **DatabaseConfiguration** (`Configuration/DatabaseConfiguration.cs`)
   - Configuration helpers
   - Server/database name extraction
   - Authentication type detection

## Usage Examples

### Testing Database Connection

**Endpoint:** `GET /api/health/status`

**Response:**
```json
{
  "status": "healthy",
  "timestamp": "2024-01-01T12:00:00Z",
  "database": {
    "connected": true,
    "info": "Server: SRIBINKK26\\SQLEXPRESS, Database: Credit"
  },
  "environment": {
    "aspnetcore": "10.0.0",
    "os": "Windows 10.0.19045"
  }
}
```

### Get Database Information

**Endpoint:** `GET /api/health/database`

**Response:**
```json
{
  "database": "Server: SRIBINKK26\\SQLEXPRESS, Database: Credit"
}
```

### Inject DatabaseService in Controllers

```csharp
[ApiController]
[Route("[controller]")]
public class MyController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public MyController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet("data")]
    public async Task<IActionResult> GetData()
    {
        var context = _databaseService.GetContext();
        var members = await context.Members.ToListAsync();
        return Ok(members);
    }
}
```

## Entities & Relationships

### Main Entities

| Entity | Key | Related To |
|--------|-----|-----------|
| **Member** | MemberNo | Region, Corporation, Charges, Payments, Statements |
| **Category** | CategoryNo | Charges |
| **Charge** | ChargeNo | Member, Category, Provider |
| **Payment** | PaymentNo | Member |
| **Statement** | StatementNo | Member |
| **Provider** | ProviderNo | Region, Charges |
| **Region** | RegionNo | Member, Corporation, Provider |
| **Corporation** | CorpNo | Region, Members |
| **Person** | Id | (User management) |

### Foreign Key Relationships

- **Payment** → Member (Many-to-One)
- **Statement** → Member (Many-to-One)
- **Charge** → Member (Many-to-One)
- **Charge** → Category (Many-to-One)
- **Charge** → Provider (Many-to-One)
- **Member** → Region (Many-to-One)
- **Member** → Corporation (Many-to-One, nullable)
- **Corporation** → Region (Many-to-One)
- **Provider** → Region (Many-to-One)

## Dependency Injection Setup

In `Program.cs`:

```csharp
// Register DbContext
builder.Services.AddDbContext<CreditDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Database Service
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
```

## Common Operations

### Retrieve All Members
```csharp
var context = _databaseService.GetContext();
var members = await context.Members.ToListAsync();
```

### Retrieve Payments for a Member
```csharp
var payments = await context.Payments
    .Where(p => p.MemberNo == memberId)
    .ToListAsync();
```

### Add New Payment
```csharp
var payment = new Payment
{
    MemberNo = 1,
    PaymentDt = DateTime.Now,
    PaymentAmt = 100.00m,
    PaymentCode = "PAY001"
};
context.Payments.Add(payment);
await context.SaveChangesAsync();
```

### Delete Payment
```csharp
var payment = await context.Payments.FindAsync(paymentId);
if (payment != null)
{
    context.Payments.Remove(payment);
    await context.SaveChangesAsync();
}
```

## Connection String Components Explained

- **Data Source**: `SRIBINKK26\SQLEXPRESS` - SQL Server instance location
- **Initial Catalog**: `Credit` - Database name
- **Integrated Security**: `True` - Windows Authentication (no username/password needed)
- **Trust Server Certificate**: `True` - Accepts self-signed certificates

## Troubleshooting

### Connection Failed
1. Verify SQL Server is running: `SRIBINKK26\SQLEXPRESS`
2. Check database exists: `Credit`
3. Confirm Windows user has database access
4. Test endpoint: `GET /api/health/status`

### Table Not Found
1. Ensure database tables match entity names
2. Verify foreign key constraints exist
3. Check table collation compatibility

### Authentication Issues
- Current setup uses **Windows Integrated Security**
- Running app must have database access permissions
- Run Visual Studio as Administrator if needed

## Best Practices

✅ Always inject `IDatabaseService` rather than `DbContext` directly  
✅ Use `async/await` for all database operations  
✅ Call `TestConnectionAsync()` on application startup  
✅ Implement proper error handling and logging  
✅ Use connection pooling (configured by default)  
✅ Validate data before SaveChangesAsync()  

## Environment Variables

Current configuration uses:
- **Development**: `appsettings.Development.json`
- **Production**: `appsettings.json`

Override connection string in production:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-production-connection-string"
  }
}
```

## References

- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server Connection Strings](https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder)
- [.NET 10 Release Notes](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10)
