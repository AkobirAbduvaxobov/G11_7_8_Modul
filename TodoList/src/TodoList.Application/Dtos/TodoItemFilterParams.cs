namespace TodoList.Application.Dtos;

public class TodoItemFilterParams
{
    public string? Search { get; set; }
    public bool? IsCompleted { get; set; }
    public DateTime? FromDueDate { get; set; }
    public DateTime? ToDueDate { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
}
