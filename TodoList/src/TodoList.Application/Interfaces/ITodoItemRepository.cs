using TodoList.Domain.Entites;

namespace TodoList.Application.Interfaces;

public interface ITodoItemRepository
{
    Task<TodoItem?> SelectByIdAsync(long todoItemId);
    IQueryable<TodoItem> SelectAll();
    Task<ICollection<TodoItem>> SelectAllAsync();
    Task InsertAsync(TodoItem todoItem);
    void Update(TodoItem todoItem);
    void Delete(TodoItem todoItem);
    Task<int> SaveChangesAsync();
}
