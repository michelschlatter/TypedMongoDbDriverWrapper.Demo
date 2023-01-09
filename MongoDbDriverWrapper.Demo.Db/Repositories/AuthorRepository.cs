using MongoDbDriverWrapper.Demo.Db.Documents;
using TypedMongoDbDriverWrapper;

namespace MongoDbDriverWrapper.Demo.Db.Repositories
{
    public class AuthorRepository : BaseRepository<Author>
    {
        public AuthorRepository(IDbContext dbContext) : base(dbContext) { }
    }
}
