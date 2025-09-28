using CqrsBook.Application.Commands.CreateBook;
using CqrsBook.Application.Dtos;
using CqrsBook.Application.Queries.GetAllBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsBook.Api.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    public BooksController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<long>> Create(CreateBookCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpGet]
    public async Task<ICollection<BookDto>> GetAll()
    {
        var command = new GetAllBooksQuery();
        var result = await _mediator.Send(command);
        return result;
    }
}