using MongoDB.Driver;
using MongoDBFirstProj.Api.Models;

namespace MongoDBFirstProj.Api.Services;

public class MongoBookService : IMongoBookService
{
    private readonly IMongoCollection<Book> _books;

    public MongoBookService(IMongoDatabase database)
    {
        _books = database.GetCollection<Book>("books");
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _books.Find(FilterDefinition<Book>.Empty).ToListAsync();
    }

    public async Task<long> AddAsync(Book book)
    {
        if (book.BookId == 0)
            book.BookId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        await _books.InsertOneAsync(book);
        return book.BookId;
    }
}
