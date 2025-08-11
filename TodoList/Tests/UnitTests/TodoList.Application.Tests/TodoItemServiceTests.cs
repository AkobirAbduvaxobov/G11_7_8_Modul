using Moq;
using TodoList.Application.Dtos;
using TodoList.Application.Interfaces;
using TodoList.Application.Services;
using TodoList.Domain.Entites;
using Xunit;

namespace TodoList.Application.Tests;

public class TodoItemServiceTests
{
    private readonly Mock<ITodoItemRepository> _mockRepo;
    private readonly TodoItemService _todoItemService;

    public TodoItemServiceTests()
    {
        _mockRepo = new Mock<ITodoItemRepository>();
        _todoItemService = new TodoItemService(_mockRepo.Object);
    }

    [Fact]
    public async Task AddAsync_ValidTodoItem_ReturnsId()
    {
        // Arrange
        var todoItemCreateDto = new TodoItemCreateDto
        {
            Title = "Test Todo",
            Description = "Test Description",
            DueDate = DateTime.UtcNow.AddDays(1)
        };

        var todoItem = new TodoItem
        {
            Title = todoItemCreateDto.Title,
            Description = todoItemCreateDto.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            DueDate = todoItemCreateDto.DueDate
        };

        _mockRepo
            .Setup(r => r.InsertAsync(It.IsAny<TodoItem>()))
            .Returns(Task.CompletedTask);

        _mockRepo
            .Setup(r => r.SaveChangesAsync())
            .Callback(() =>
            {
               
                var insertedItem = _mockRepo.Invocations
                    .FirstOrDefault(inv => inv.Method.Name == nameof(_mockRepo.Object.InsertAsync))
                    ?.Arguments.FirstOrDefault() as TodoItem;

                if (insertedItem != null)
                {
                    insertedItem.TodoItemId = 4;
                }
            })
            .ReturnsAsync(1);



        // Act
        var id = await _todoItemService.AddAsync(todoItemCreateDto);

        // Assert
        Assert.Equal(4, id);
    }

    [Fact]
    public async Task GetAllAsync_WhenCalled_ReturnsMappedDtos()
    {
        // Arrange
        var todoItems = new List<TodoItem>
        {
            new TodoItem
            {
                TodoItemId = 1,
                Title = "Task 1",
                Description = "Description 1",
                IsCompleted = false,
                CreatedAt = new DateTime(2025, 8, 10),
                DueDate = new DateTime(2025, 8, 15)
            },
            new TodoItem
            {
                TodoItemId = 2,
                Title = "Task 2",
                Description = "Description 2",
                IsCompleted = true,
                CreatedAt = new DateTime(2025, 8, 9),
                DueDate = new DateTime(2025, 8, 20)
            }
        };

        _mockRepo
            .Setup(r => r.SelectAllAsync())
            .ReturnsAsync(todoItems);

        // Act
        var result = await _todoItemService.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count);

        var first = result.First();
        Assert.Equal(1, first.TodoItemId);
        Assert.Equal("Task 1", first.Title);
        Assert.Equal("Description 1", first.Description);
        Assert.False(first.IsCompleted);
        Assert.Equal(new DateTime(2025, 8, 10), first.CreatedAt);
        Assert.Equal(new DateTime(2025, 8, 15), first.DueDate);

        var second = result.Last();
        Assert.Equal(2, second.TodoItemId);
        Assert.Equal("Task 2", second.Title);
        Assert.Equal("Description 2", second.Description);
        Assert.True(second.IsCompleted);
        Assert.Equal(new DateTime(2025, 8, 9), second.CreatedAt);
        Assert.Equal(new DateTime(2025, 8, 20), second.DueDate);
    }
}
