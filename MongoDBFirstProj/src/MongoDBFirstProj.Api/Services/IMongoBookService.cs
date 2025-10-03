using MongoDBFirstProj.Api.Models;

namespace MongoDBFirstProj.Api.Services
{
    public interface IMongoBookService
    {
        Task<List<Book>> GetAllAsync();
        Task<long> AddAsync(Book book);
    }
}