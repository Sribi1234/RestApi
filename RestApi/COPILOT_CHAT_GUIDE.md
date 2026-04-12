# Copilot Chat - SQL Server Database Integration Guide

## 🎯 Quick Start Prompts

### 1. Health Check
**Prompt:** "Check if the SQL Server database is connected"
**Implementation:**
```csharp
var isConnected = await _databaseService.TestConnectionAsync();
```

### 2. Retrieve Data
**Prompt:** "Get all members from the database"
**Implementation:**
```csharp
var context = _databaseService.GetContext();
var members = await context.Members.ToListAsync();
```

**Prompt:** "Get payments for member ID 5"
**Implementation:**
```csharp
var payments = await _databaseService.GetContext().Payments
    .Where(p => p.MemberNo == 5)
    .ToListAsync();
```

### 3. Create Records
**Prompt:** "Add a new payment of $500 for member 1"
**Implementation:**
```csharp
var context = _databaseService.GetContext();
context.Payments.Add(new Payment 
{ 
    MemberNo = 1,
    PaymentDt = DateTime.Now,
    PaymentAmt = 500,
    PaymentCode = "PAY001"
});
await context.SaveChangesAsync();
```

### 4. Update Records
**Prompt:** "Update payment 10 to $600"
**Implementation:**
```csharp
var context = _databaseService.GetContext();
var payment = await context.Payments.FindAsync(10);
if (payment != null) {
    payment.PaymentAmt = 600;
    await context.SaveChangesAsync();
}
```

### 5. Delete Records
**Prompt:** "Delete payment with ID 10"
**Implementation:**
```csharp
var context = _databaseService.GetContext();
var payment = await context.Payments.FindAsync(10);
if (payment != null) {
    context.Payments.Remove(payment);
    await context.SaveChangesAsync();
}
```

---

## 📊 Data Access Patterns

### Pattern 1: Single Entity Retrieval
```csharp
var member = await _databaseService.GetContext()
    .Members
    .FirstOrDefaultAsync(m => m.MemberNo == memberId);
```

### Pattern 2: Filtered List
```csharp
var expiredMembers = await _databaseService.GetContext()
    .Members
    .Where(m => m.ExprDt < DateTime.Now)
    .ToListAsync();
```

### Pattern 3: Include Related Data
```csharp
var member = await _databaseService.GetContext()
    .Members
    .Include(m => m.Payments)
    .Include(m => m.Statements)
    .FirstOrDefaultAsync(m => m.MemberNo == memberId);
```

### Pattern 4: Aggregation
```csharp
var totalPayments = await _databaseService.GetContext()
    .Payments
    .Where(p => p.MemberNo == memberId)
    .SumAsync(p => p.PaymentAmt);
```

### Pattern 5: Pagination
```csharp
int pageNumber = 1;
int pageSize = 10;
var members = await _databaseService.GetContext()
    .Members
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();
```

---

## 🔍 Advanced Queries

### Query: Members with Overdue Payments
```csharp
var overdueMembers = await _databaseService.GetContext()
    .Members
    .Where(m => m.Statements.Any(s => s.DueDt < DateTime.Now))
    .Include(m => m.Statements)
    .ToListAsync();
```

### Query: Total Payments by Month
```csharp
var paymentsByMonth = await _databaseService.GetContext()
    .Payments
    .GroupBy(p => new { p.PaymentDt.Year, p.PaymentDt.Month })
    .Select(g => new 
    { 
        Year = g.Key.Year,
        Month = g.Key.Month,
        Total = g.Sum(p => p.PaymentAmt)
    })
    .ToListAsync();
```

### Query: Members by Region
```csharp
var membersByRegion = await _databaseService.GetContext()
    .Regions
    .Include(r => r.Members)
    .ToListAsync();
```

---

## 🛠️ Common Controller Patterns

### Pattern: Get All (with Pagination)
```csharp
[HttpGet]
public async Task<IActionResult> GetMembers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
{
    var members = await _databaseService.GetContext()
        .Members
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    return Ok(members);
}
```

### Pattern: Get By ID
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetMember(int id)
{
    var member = await _databaseService.GetContext()
        .Members
        .FirstOrDefaultAsync(m => m.MemberNo == id);
    if (member == null) return NotFound();
    return Ok(member);
}
```

### Pattern: Create with Validation
```csharp
[HttpPost]
public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
{
    if (payment?.PaymentAmt <= 0) return BadRequest("Invalid amount");
    
    var context = _databaseService.GetContext();
    context.Payments.Add(payment);
    await context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentNo }, payment);
}
```

### Pattern: Update with Validation
```csharp
[HttpPut("{id}")]
public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment payment)
{
    var context = _databaseService.GetContext();
    var existing = await context.Payments.FindAsync(id);
    if (existing == null) return NotFound();
    
    existing.PaymentAmt = payment.PaymentAmt;
    await context.SaveChangesAsync();
    return Ok(existing);
}
```

### Pattern: Delete with Safety Check
```csharp
[HttpDelete("{id}")]
public async Task<IActionResult> DeletePayment(int id)
{
    var context = _databaseService.GetContext();
    var payment = await context.Payments.FindAsync(id);
    if (payment == null) return NotFound();
    
    context.Payments.Remove(payment);
    await context.SaveChangesAsync();
    return NoContent();
}
```

---

## 📋 Database Schema Quick Reference

### Core Tables
| Table | PK | Key Fields |
|-------|----|----|
| **Members** | MemberNo | Lastname, Firstname, City, Country |
| **Payments** | PaymentNo | MemberNo(FK), PaymentDt, PaymentAmt |
| **Statements** | StatementNo | MemberNo(FK), StatementDt, StatementAmt |
| **Charges** | ChargeNo | MemberNo(FK), CategoryNo(FK), ProviderNo(FK) |

### Lookup Tables
| Table | PK | Purpose |
|-------|----|----|
| **Category** | CategoryNo | Categorize charges |
| **Provider** | ProviderNo | Service providers |
| **Region** | RegionNo | Geographic regions |
| **Corporation** | CorpNo | Corporate entities |

### Admin Table
| Table | PK | Purpose |
|-------|----|----|
| **Person** (users) | Id | User management |

---

## 🔐 Connection String

**Development:**
```
Data Source=SRIBINKK26\SQLEXPRESS;Initial Catalog=Credit;Integrated Security=True;Trust Server Certificate=True
```

**Server Details:**
- Server: SRIBINKK26\SQLEXPRESS
- Database: Credit
- Auth: Windows Integrated Security

---

## 🎯 Use Cases & Solutions

### Use Case 1: Daily Revenue Report
**Prompt:** "Calculate total revenue for today"
```csharp
var today = DateTime.Today;
var todayRevenue = await _databaseService.GetContext()
    .Payments
    .Where(p => p.PaymentDt.Date == today)
    .SumAsync(p => p.PaymentAmt);
```

### Use Case 2: Overdue Accounts
**Prompt:** "Get all members with overdue statements"
```csharp
var overdueAccounts = await _databaseService.GetContext()
    .Members
    .Where(m => m.Statements.Any(s => s.DueDt < DateTime.Now))
    .ToListAsync();
```

### Use Case 3: Member Analysis
**Prompt:** "List members with high payment activity"
```csharp
var activeMembers = await _databaseService.GetContext()
    .Members
    .Where(m => m.Payments.Count > 10)
    .Include(m => m.Payments)
    .ToListAsync();
```

### Use Case 4: Provider Performance
**Prompt:** "Get total charges by provider"
```csharp
var providerStats = await _databaseService.GetContext()
    .Providers
    .Select(p => new {
        Provider = p.ProviderName,
        TotalCharges = p.Charges.Sum(c => c.ChargeAmt),
        ChargeCount = p.Charges.Count
    })
    .ToListAsync();
```

---

## 🚀 Running the Application

1. **Start the application:**
   ```
   dotnet run
   ```

2. **Test the connection:**
   ```
   GET https://localhost:5000/api/health/status
   ```

3. **Access Swagger UI:**
   ```
   https://localhost:5000/swagger
   ```

4. **View logs:**
   ```
   RestApi/Data/myapp-[date].txt
   ```

---

## 📚 Service Injection Quick Reference

### In Constructor
```csharp
public MyController(IDatabaseService databaseService)
{
    _databaseService = databaseService;
}
```

### Use in Method
```csharp
public async Task<IActionResult> MyMethod()
{
    var context = _databaseService.GetContext();
    // Use context here
}
```

### Test Connection
```csharp
public async Task<IActionResult> CheckHealth()
{
    var connected = await _databaseService.TestConnectionAsync();
    return Ok(new { connected });
}
```

---

## 🔗 Related Files

- Configuration: `appsettings.Development.json`
- Context: `RestApi/Data/CreditDbContext.cs`
- Service: `RestApi/Services/DatabaseService.cs`
- Health: `RestApi/Controllers/HealthController.cs`
- Models: `RestApi/Models/[EntityName].cs`

---

## ✅ Build & Deploy Status

- ✅ Build Status: Successful
- ✅ Connection: SRIBINKK26\SQLEXPRESS
- ✅ Database: Credit
- ✅ ORM: Entity Framework Core
- ✅ Framework: .NET 10

---

**Ready to use! Ask Copilot Chat about any database operation with the patterns above.** 🚀
