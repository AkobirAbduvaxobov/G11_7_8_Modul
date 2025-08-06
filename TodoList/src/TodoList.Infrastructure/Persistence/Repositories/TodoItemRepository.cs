using Microsoft.EntityFrameworkCore;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entites;

namespace TodoList.Infrastructure.Persistence.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly AppDbContext _appDbContext;

    public TodoItemRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Delete(TodoItem todoItem)
    {
        _appDbContext.TodoItems.Remove(todoItem);
    }

    public async Task InsertAsync(TodoItem todoItem)
    {
        await _appDbContext.TodoItems.AddAsync(todoItem);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }

    public IQueryable<TodoItem> SelectAll()
    {
        return _appDbContext.TodoItems;
    }

    public async Task<ICollection<TodoItem>> SelectAllAsync()
    {
        var todos = await _appDbContext.TodoItems.ToListAsync();
        return todos;
    }

    public async Task<TodoItem?> SelectByIdAsync(long todoItemId)
    {
        var todItem = await _appDbContext.TodoItems
            .FirstOrDefaultAsync(ti => ti.TodoItemId == todoItemId);

        return todItem;
    }

    public void Update(TodoItem todoItem)
    {
        _appDbContext.TodoItems.Update(todoItem);
    }
}
