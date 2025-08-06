using TodoList.Application.Dtos;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entites;

namespace TodoList.Application.Services;

public class TodoItemService : ITodoItemService
{
    private readonly ITodoItemRepository _todoItemRepository;

    public TodoItemService(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task<long> AddAsync(TodoItemCreateDto todoItemCreateDto)
    {
        var todoItem = new TodoItem
        {
            Title = todoItemCreateDto.Title,
            Description = todoItemCreateDto.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            DueDate = todoItemCreateDto.DueDate
        };  

        await _todoItemRepository.InsertAsync(todoItem);
        await _todoItemRepository.SaveChangesAsync();
        return todoItem.TodoItemId;
    }

    public async Task<ICollection<TodoItemGetDto>> GetAllAsync()
    {
        var todoItems = await _todoItemRepository.SelectAllAsync(); 
        var todoItemDtos = todoItems.Select(t => new TodoItemGetDto
        {
            TodoItemId = t.TodoItemId,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted,
            CreatedAt = t.CreatedAt,
            DueDate = t.DueDate
        }).ToList();

        return todoItemDtos;
    }

    public async Task DeleteAsync(long todoItemId)
    {
        var todoItem = await _todoItemRepository.SelectByIdAsync(todoItemId);
        if (todoItem == null)
        {
            throw new Exception($"Todo item with ID {todoItemId} not found to delete.");
        }

        _todoItemRepository.Delete(todoItem);
        await _todoItemRepository.SaveChangesAsync();
    }

    public async Task<TodoItemGetDto> GetByIdAsync(long todoItemId)
    {
        var todoItem = await _todoItemRepository.SelectByIdAsync(todoItemId);
        if (todoItem == null)
        {
            throw new Exception($"Todo item with ID {todoItemId} not found.");
        }

        var todoItemDto = new TodoItemGetDto
        {
            TodoItemId = todoItem.TodoItemId,
            Title = todoItem.Title,
            Description = todoItem.Description,
            IsCompleted = todoItem.IsCompleted,
            CreatedAt = todoItem.CreatedAt,
            DueDate = todoItem.DueDate
        };

        return todoItemDto;
    }

    public async Task UpdateAsync(long todoItemId, TodoItemUpdateDto todoItemUpdateDto)
    {
        var todoItem = await _todoItemRepository.SelectByIdAsync(todoItemId);
        if (todoItem == null)
        {
            throw new Exception($"Todo item with ID {todoItemId} not found to update");
        }

        todoItem.Title = todoItemUpdateDto.Title;
        todoItem.Description = todoItemUpdateDto.Description;
        todoItem.IsCompleted = todoItemUpdateDto.IsCompleted;
        todoItem.DueDate = todoItemUpdateDto.DueDate;

        _todoItemRepository.Update(todoItem);
        await _todoItemRepository.SaveChangesAsync();
    }

    public TodoItemListResult GetPagedFiltered(TodoItemFilterParams todoItemFilterParams)
    {
        var query = _todoItemRepository.SelectAll();

        if (todoItemFilterParams.Search != null)
        {
            query = query.Where(t => t.Title.Contains(todoItemFilterParams.Search));
        }

        if (todoItemFilterParams.IsCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == todoItemFilterParams.IsCompleted);
        }

        if (todoItemFilterParams.FromDueDate.HasValue)
        {
            query = query.Where(t => t.DueDate > todoItemFilterParams.FromDueDate);
        }

        if (todoItemFilterParams.ToDueDate.HasValue)
        {
            query = query.Where(t => t.DueDate < todoItemFilterParams.ToDueDate);
        }

        if (todoItemFilterParams.Skip < 0)
        {
            todoItemFilterParams.Skip = 0;
        }

        if (todoItemFilterParams.Take > 20 || todoItemFilterParams.Take < 0)
        {
            todoItemFilterParams.Take = 10;
        }

        query = query.Skip(todoItemFilterParams.Skip).Take(todoItemFilterParams.Take);

        var toDoItems = query.ToList();

        var toDoItemDtos = toDoItems.Select(t => new TodoItemGetDto
        {
            TodoItemId = t.TodoItemId,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted,
            CreatedAt = t.CreatedAt,
            DueDate = t.DueDate
        }).ToList();

        var totalCount = _todoItemRepository.SelectAll().Count();

        var todoItemListResult = new TodoItemListResult()
        {
            TodoItemGetDtos = toDoItemDtos,
            TotalCount = totalCount,
        };

        return todoItemListResult;
    }


}
