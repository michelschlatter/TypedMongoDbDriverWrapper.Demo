using System;
using System.Collections.Generic;
using System.Linq;
using MongoDbDriverWrapper.Demo.Db.Documents;
using TypedMongoDbDriverWrapper;

namespace MongoDbDriverWrapper.Demo.Db.Settings
{
    public record DocumentCollection(Type DocumentType, string CollectionName) : IDocumentCollection;
    public class CollectionProvider : ICollectionProvider
    {
        public ICollection<IDocumentCollection> GetAll()
        {
            return new List<IDocumentCollection>
            {
                new DocumentCollection(typeof(Book), "Books"),
                new DocumentCollection(typeof(Author), "Authors")
            };
        }
    }
}
