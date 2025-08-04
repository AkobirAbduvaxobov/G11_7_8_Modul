using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entites;
using TodoList.Infrastructure.Persistence.Configurations;

namespace TodoList.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TodoItemConfigurations());
    }
}
