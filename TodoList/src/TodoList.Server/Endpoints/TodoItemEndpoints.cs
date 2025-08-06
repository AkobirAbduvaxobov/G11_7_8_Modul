using TodoList.Application.Dtos;
using TodoList.Application.Services;

namespace TodoList.Server.Endpoints;

public static class TodoItemEndpoints
{
    public static RouteGroupBuilder MapTodoItemEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/v1/api/todoitems")
                          .WithTags("Todo Items");

        
        group.MapPost("/", async (ITodoItemService service, TodoItemCreateDto dto) =>
        {
            var createdId = await service.AddAsync(dto);
            return createdId;
        });

        
        group.MapGet("/", (ITodoItemService service, [AsParameters] TodoItemFilterParams filter) =>
        {
            var result = service.GetPagedFiltered(filter);
            return result;
        });


        group.MapGet("/{id:long}", async (ITodoItemService service, long id) =>
        {
            var item = await service.GetByIdAsync(id);
            return item;
        });


        group.MapPut("/{id:long}", async (ITodoItemService service, long id, TodoItemUpdateDto dto) =>
        {
            await service.UpdateAsync(id, dto);
        });


        group.MapDelete("/{id:long}", async (ITodoItemService service, long id) =>
        {
            await service.DeleteAsync(id);
        });

        return group;
    }
}