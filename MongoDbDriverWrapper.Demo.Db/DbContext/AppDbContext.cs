using System;
using MongoDB.Driver;
using MongoDbDriverWrapper.Demo.Db.Documents;
using MongoDbDriverWrapper.Demo.Db.Settings;

namespace MongoDbDriverWrapper.Demo.Db.DbContext
{
    public class AppDbContext : TypedMongoDbDriverWrapper.DbContext
    {
        public AppDbContext(string dbName, string connectionString) :
            base(dbName,
                connectionString,
                new CollectionProvider())
        {
        }

        public IMongoCollection<Book> Books => GetCollection<Book>();
        public IMongoCollection<Author> Authors => GetCollection<Author>();

    }
}
