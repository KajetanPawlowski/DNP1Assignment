using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class GossipsDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Gossips.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); 
    }
    
}