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

}
