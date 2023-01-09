using MongoDbDriverWrapper.Demo.Db.Documents;

namespace MongoDbDriverWrapper.Demo.Db.Aggregations
{
    public class BookWithAuthor : Book
    {
        //MongoDbDriver handles only arrays
        // write a custom serializer or leave it like this
        public Author[]? Authors { get; set; }
    }
}
