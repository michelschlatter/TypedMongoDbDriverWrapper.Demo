using MongoDB.Driver;
using MongoDbDriverWrapper.Demo.Db.Documents;
using TypedMongoDbDriverWrapper;

namespace MongoDbDriverWrapper.Demo.Db.Settings
{
    public class IndexFactory : IIndexFactory
    {
        public async Task CreateIndicesAsync(IDbContext dbContext)
        {
            await CreateBookIndices(dbContext);
        }

        private static async Task CreateBookIndices(IDbContext dbContext)
        {
            var indexBuilder = Builders<Book>.IndexKeys;
            var index = new CreateIndexModel<Book>(indexBuilder
                .Ascending(x => x.AuthorId));
            await dbContext.GetCollection<Book>().Indexes.CreateOneAsync(index);
        }
    }
}
