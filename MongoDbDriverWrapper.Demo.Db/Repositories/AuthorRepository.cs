using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDbDriverWrapper.Demo.Db.Documents;
using TypedMongoDbDriverWrapper;

namespace MongoDbDriverWrapper.Demo.Db.Repositories
{
    public class AuthorRepository : BaseRepository<Author>
    {
        private readonly ILogger<AuthorRepository> _logger;

        public AuthorRepository(IDbContext dbContext, ILogger<AuthorRepository> logger) : base(dbContext)
        {
            _logger = logger;
        }

        
        public override async Task ReplaceOneAsync(Author author)
        {
            // a transaction is needed, because if an update (author or book) fails, all the changes should be rolled back
            var transaction = DbContext.StartTransectionSessionSync();

            try
            {
                //start the transaction
                transaction.StartTransaction();

                //update author name in books
                var bookUpdate = Builders<Book>.Update.Set(b => b.AuthorFullName, author.FullName);
                await DbContext.GetCollection<Book>().UpdateManyAsync(transaction, //add transaction handle
                    b => b.AuthorId == author.Id, // filter
                    bookUpdate); //update cmd

                await DbContext.GetCollection<Author>().ReplaceOneAsync(transaction, //add transaction handle
                    a => a.Id == author.Id, // filter
                    author); //replace with author

                // Commit
                await transaction.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Catched Exception, rolling back transaction in {GetType().Name}.{nameof(ReplaceOneAsync)}");
                await transaction.AbortTransactionAsync();
            }
        }

    }
}
