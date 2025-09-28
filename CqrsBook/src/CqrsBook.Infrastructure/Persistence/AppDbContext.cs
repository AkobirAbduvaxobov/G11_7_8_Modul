using CqrsBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CqrsBook.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}