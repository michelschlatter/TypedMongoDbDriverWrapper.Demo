using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDbDriverWrapper.Demo.Db.Aggregations;
using MongoDbDriverWrapper.Demo.Db.Documents;
using MongoDbDriverWrapper.Demo.Db.Repositories;

namespace MongoDbDriverWrapper.Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
       
        private readonly BookRepository _bookRepository;
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger, 
            BookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IList<Book>> Get()
        {
            _logger.LogTrace($"Received Request in {nameof(BooksController)}.{nameof(Get)}");
            return await _bookRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Book> Get(string id)
        {
            _logger.LogTrace($"Received Request in {nameof(BooksController)}.{nameof(Get)} by Id");
            return await _bookRepository.GetSingleOrThrowAsync(id);
        }

        [HttpGet("publisher/{name}")]
        public async Task<IList<Book>> GetByPublisherName(string name)
        {
            _logger.LogTrace($"Received Request in {nameof(BooksController)}.{nameof(GetByPublisherName)}");
            return await _bookRepository.GetByPublisherNameAsync(name);
        }

        [HttpGet("search")]
        public async Task<IList<BookWithAuthor>> GetByAuthors([FromQuery] List<string> authorIds, [FromQuery] int? maxAge)
        {
            _logger.LogTrace($"Received Request in {nameof(BooksController)}.{nameof(GetByAuthors)}");
            return await _bookRepository.GetBooksByAuthors(authorIds, maxAge);
        }

        [HttpGet("ids")]
        public async Task<List<ObjectId>> GetAllIds()
        {
            _logger.LogTrace($"Received Request in {nameof(BooksController)}.{nameof(GetAllIds)}");
            return await _bookRepository.GetAllIds();
        }

        [HttpPost]
        public async Task<Book> Insert(Book book)
        {
            _logger.LogTrace($"Received Request in {nameof(BooksController)}.{nameof(Insert)}");

            if (book.Id == ObjectId.Empty) book.Id = ObjectId.GenerateNewId();
            await _bookRepository.InsertOneAsync(book);
            return await _bookRepository.GetSingleOrThrowAsync(book.Id.ToString()); ;
        }

        [HttpPut("{id}")]
        public async Task<Book> UpdateBookVersion(string id, int version)
        {
            _logger.LogTrace($"Received Request in {nameof(BooksController)}.{nameof(UpdateBookVersion)}");

            await _bookRepository.UpdateVersionAsync2(id, version);
            return await _bookRepository.GetSingleOrThrowAsync(id);
        }

        [HttpDelete]
        public async Task DeleteAll()
        {
            _logger.LogTrace($"Received Request in {nameof(BooksController)}.{nameof(DeleteAll)}");
            await _bookRepository.DeleteAllAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            _logger.LogTrace($"Received Request in {nameof(BooksController)}.{nameof(Delete)}");
            await _bookRepository.DeleteByIdAsync(id);
        }
    }
}