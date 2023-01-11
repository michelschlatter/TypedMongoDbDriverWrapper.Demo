using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDbDriverWrapper.Demo.Db.Documents;
using MongoDbDriverWrapper.Demo.Db.Repositories;

namespace MongoDbDriverWrapper.Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
       
        private readonly AuthorRepository _authorRepository;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(ILogger<AuthorsController> logger,
            AuthorRepository authorRepository)
        {
            _logger = logger;
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IList<Author>> Get()
        {
            _logger.LogTrace($"Received Request in {nameof(AuthorsController)}.{nameof(Get)}");
            return await _authorRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Author> Get(string id)
        {
            _logger.LogTrace($"Received Request in {nameof(AuthorsController)}.{nameof(Get)} by Id");
            return await _authorRepository.GetSingleOrThrowAsync(id);
        }

        [HttpPost]
        public async Task<Author> Insert(Author author)
        {
            _logger.LogTrace($"Received Request in {nameof(AuthorsController)}.{nameof(Insert)}");

            if (author.Id == ObjectId.Empty) author.Id = ObjectId.GenerateNewId();
            await _authorRepository.InsertOneAsync(author);
            return await _authorRepository.GetSingleOrThrowAsync(author.Id.ToString());
        }

        [HttpPut]
        public async Task<Author> Update(Author author)
        {
            _logger.LogTrace($"Received Request in {nameof(AuthorsController)}.{nameof(Update)}");

            if (author.Id == ObjectId.Empty) throw new Exception($"{nameof(author.Id)} can not be null or empty");
            await _authorRepository.ReplaceOneAsync(author);
            return await _authorRepository.GetSingleOrThrowAsync(author.Id.ToString());
        }
    }
}