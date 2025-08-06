using TodoList.Application.Dtos;

namespace TodoList.Application.Services;

public interface ITodoItemService
{
    Task<long> AddAsync(TodoItemCreateDto todoItemCreateDto);
    Task<ICollection<TodoItemGetDto>> GetAllAsync();
    Task<TodoItemGetDto> GetByIdAsync(long todoItemId);
    Task DeleteAsync(long todoItemId);
    Task UpdateAsync(long todoItemId, TodoItemUpdateDto todoItemUpdateDto);
    TodoItemListResult GetPagedFiltered(TodoItemFilterParams todoItemFilterParams);
}