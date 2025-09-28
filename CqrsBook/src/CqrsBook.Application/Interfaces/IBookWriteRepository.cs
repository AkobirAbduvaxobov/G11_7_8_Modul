using CqrsBook.Domain.Entities;

namespace CqrsBook.Application.Interfaces;

public interface IBookWriteRepository
{
    Task<long> AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
}
