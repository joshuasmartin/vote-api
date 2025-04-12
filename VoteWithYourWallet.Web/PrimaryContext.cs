using Microsoft.EntityFrameworkCore;
using VoteWithYourWallet.Common.Models;

namespace VoteWithYourWallet.Web;

public class PrimaryContext : DbContext
{
    public DbSet<Score>? Scores { get; set; }
    
    public DbSet<Subject>? Subjects { get; set; }
    
    public DbSet<User>? Users { get; set; }
    
    [ActivatorUtilitiesConstructor]
    public PrimaryContext(DbContextOptions<PrimaryContext> options)
        : base(options)
    {
    }

    public PrimaryContext(string connectionString) : base(GetOptions(connectionString))
    {
    }

    private static DbContextOptions GetOptions(string connectionString)
    {
        return new DbContextOptionsBuilder().UseNpgsql(connectionString).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
