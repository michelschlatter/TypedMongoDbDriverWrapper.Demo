using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDbDriverWrapper.Demo.Db.Aggregations;
using MongoDbDriverWrapper.Demo.Db.DbContext;
using MongoDbDriverWrapper.Demo.Db.Documents;
using TypedMongoDbDriverWrapper;
using TypedMongoDbDriverWrapper.Exceptions;

namespace MongoDbDriverWrapper.Demo.Db.Repositories
{
    // Implement a Interface for each Repository for Testing Purposes!
    public class BookRepository : BaseRepository<Book>
    {
        private readonly AppDbContext _appDbContext;
        public BookRepository(IDbContext dbContext) : base(dbContext)
        {
            _appDbContext = (AppDbContext)dbContext;
        }

        public async Task<Book> UpdateVersionAsync(string id, int version)
        {
            var book = await GetSingleOrThrowAsync(id);
            book.Version = version;
            await ReplaceOneAsync(book);
            return await GetSingleOrThrowAsync(id);
        }

        public async Task<Book> UpdateVersionAsync2(string id, int version)
        {
           var updateCmd = Builders<Book>.Update
                .Set(b => b.Version, version);
           var update = await _appDbContext.Books.UpdateOneAsync(b => b.Id == ObjectId.Parse(id), updateCmd);
          //or: var update = await DbContext.GetCollection<Book>().UpdateOneAsync(b => b.Id == ObjectId.Parse(id), updateCmd);
          if (update.ModifiedCount != 1) throw new DbNotModifiedException(update.ModifiedCount, 1, nameof(UpdateVersionAsync2));
          return await GetSingleOrThrowAsync(id);
        }

        public async Task<List<Book>> GetByPublisherNameAsync(string publisherName)
        {
           return await _appDbContext.Books
                .AsQueryable()
                .Where(b => b.Publisher != null && b.Publisher.Name == publisherName)
                .ToListAsync();
        }

        public async Task<List<ObjectId>> GetAllIds()
        { 
            // Projection 
           return await _appDbContext.Books.AsQueryable()
                .Select(b => b.Id)
                .ToListAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            await DeleteAsync(id);
        }

        public async Task DeleteAllAsync()
        {
            await DeleteManyAsync(await GetAllIds());
        }

        public async Task<List<BookWithAuthor>> GetBooksByAuthors(List<string> authorIds, int? maxAgeOfAuthor = null)
        {
            var aggregation = DbContext.GetCollection<BookWithAuthor>().Aggregate();
            var filter = Builders<BookWithAuthor>.Filter;
            var sort = Builders<BookWithAuthor>.Sort;

            //Adding Match Stage
            aggregation = aggregation.AppendStage(PipelineStageDefinitionBuilder.Match(
                filter.In(b => b.AuthorId, authorIds.Select(ObjectId.Parse)))
            );

            //Adding Author Join
            aggregation = aggregation.AppendStage(PipelineStageDefinitionBuilder
                .Lookup(DbContext.GetCollection<Author>(),
                    (BookWithAuthor x) => x.AuthorId,
                    (Author a) => a.Id,
                    (BookWithAuthor bw) => bw.Authors,
                    new AggregateLookupOptions<Author, BookWithAuthor>()));

            if (maxAgeOfAuthor != null)
            {
                //Adding Match Stage
                int maxYearBorn = DateTime.Now.Year - maxAgeOfAuthor.Value;
                aggregation = aggregation.AppendStage(PipelineStageDefinitionBuilder.Match(
                    filter.Gte(b => b.Authors![0].YearBorn, maxYearBorn))
                );
            }

            // Adding Sort Stage
            aggregation = aggregation.Sort(sort.Ascending(b => b.Title));
            return await aggregation.ToListAsync();
        }
    }
}
