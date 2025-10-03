using Microsoft.AspNetCore.Mvc;
using MongoDBFirstProj.Api.Models;
using MongoDBFirstProj.Api.Services;

namespace MongoDBFirstProj.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IMongoBookService _mongoBookService;


        public WeatherForecastController(IMongoBookService mongoBookService)
        {
            _mongoBookService = mongoBookService;
        }


        [HttpPost]
        public async Task<long> Post(Book book)
        {
            var bookId = await _mongoBookService.AddAsync(book);
            return bookId;
        }


        [HttpGet]
        public async Task<List<Book>> Get()
        {
            return await _mongoBookService.GetAllAsync();
        }
    }
}
