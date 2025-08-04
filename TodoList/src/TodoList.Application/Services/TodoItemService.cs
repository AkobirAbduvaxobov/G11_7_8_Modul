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
        throw new NotImplementedException();
    }

    public async Task<TodoItemListResult> GetPagedFilteredToDoItemsAsync(TodoItemFilterParams todoItemFilterParams)
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

        var totalCount = _toDoItemRepository.SelectAllToDoItems()
            .Where(x => x.UserId == userId)
            .Count();

        var toDoItemDtos = toDoItems
            .Select(item => _mapper.Map<ToDoItemGetDto>(item))
            .ToList();

        var getAllResponseModel = new GetAllResponseModel()
        {
            ToDoItemGetDtos = toDoItemDtos,
            TotalCount = totalCount,
        };

        return getAllResponseModel;
    }
}
