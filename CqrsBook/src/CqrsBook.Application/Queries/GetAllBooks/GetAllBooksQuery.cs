using CqrsBook.Application.Dtos;
using MediatR;

namespace CqrsBook.Application.Queries.GetAllBooks;


public record GetAllBooksQuery() : IRequest<ICollection<BookDto>>;