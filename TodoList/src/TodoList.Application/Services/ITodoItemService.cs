using TodoList.Application.Dtos;

namespace TodoList.Application.Services;

public interface ITodoItemService
{
    Task<long> AddAsync(TodoItemCreateDto todoItemCreateDto);
    Task<ICollection<TodoItemGetDto>> GetAllAsync();
    Task<TodoItemListResult> GetPagedFilteredToDoItemsAsync(TodoItemFilterParams todoItemFilterParams);
}