using CqrsBook.Application.Dtos;
using CqrsBook.Application.Interfaces;
using MediatR;

namespace CqrsBook.Application.Queries.GetAllBooks;

public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, ICollection<BookDto>>
{
    private readonly IBookReadRepository _readRepo;

    public GetAllBooksHandler(IBookReadRepository readRepo)
    {
        _readRepo = readRepo;
    }

    public async Task<ICollection<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {

        var books = await _readRepo.GetAllAsync();
        var bookDtos = books.Select(b => new BookDto
        {
            BookId = b.BookId,
            Title = b.Title,
            Author = b.Author,
            ISBN = b.ISBN,
            Publisher = b.Publisher,
            PublishDate = b.PublishDate,
            Pages = b.Pages,
            Genre = b.Genre,
            Language = b.Language,
            Price = b.Price
        }).ToList();

        return bookDtos;
    }
}
