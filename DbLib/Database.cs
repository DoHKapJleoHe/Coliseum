using Microsoft.EntityFrameworkCore;

namespace DbLib;

public class DeckDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<DeckEntity> Decks { get; set; }

    public DeckDbContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=decks.db");
    }
}