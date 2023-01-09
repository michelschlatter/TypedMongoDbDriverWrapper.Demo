using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TypedMongoDbDriverWrapper;

namespace MongoDbDriverWrapper.Demo.Db.Documents
{
    public abstract class BaseDocument : IDocument
    {
        protected BaseDocument()
        {
            Id = ObjectId.GenerateNewId();
        }

        protected BaseDocument(string? id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Id = ObjectId.Parse(id);
            }
        }

        protected BaseDocument(ObjectId id)
        {
            Id = id;
        }

        [BsonId]
        public ObjectId Id { get; set; }

    }
}
