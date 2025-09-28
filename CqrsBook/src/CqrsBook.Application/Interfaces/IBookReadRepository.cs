using CqrsBook.Domain.Entities;

namespace CqrsBook.Application.Interfaces;

public interface IBookReadRepository
{
    Task<Book?> GetByIdAsync(long id);
    Task<List<Book>> GetAllAsync();
}
