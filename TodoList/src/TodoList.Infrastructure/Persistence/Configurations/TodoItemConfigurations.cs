using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entites;

namespace TodoList.Infrastructure.Persistence.Configurations;

public class TodoItemConfigurations : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItems");
        builder.HasKey(t => t.TodoItemId);
        builder.Property(t => t.Title).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Description).HasMaxLength(251);
        builder.Property(t => t.IsCompleted).IsRequired();
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.DueDate).IsRequired();
    }
}
