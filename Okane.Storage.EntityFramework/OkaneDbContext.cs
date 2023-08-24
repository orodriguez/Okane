using Microsoft.EntityFrameworkCore;
using Okane.Core.Entities;

namespace Okane.Storage.EntityFramework;

public class OkaneDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    public OkaneDbContext()
    {
    }

    public OkaneDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=OkaneDev;Username=postgres;Password=1234;");
    }
}