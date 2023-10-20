using Microsoft.EntityFrameworkCore;

namespace DbLib;

public class DeckDbContext : DbContext
{
    public DbSet<DeckEntity> Decks { get; set; }
    public DeckDbContext(DbContextOptions<DeckDbContext> options) : base(options) { }
}