namespace TodoList.Application.Dtos;

public class TodoItemListResult
{
    public int TotalCount { get; set; }
    public ICollection<TodoItemGetDto> TodoItemGetDtos { get; set; }
}
