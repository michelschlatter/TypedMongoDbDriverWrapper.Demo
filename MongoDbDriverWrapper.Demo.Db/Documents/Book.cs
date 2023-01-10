using MongoDB.Bson;
using MongoDbDriverWrapper.Demo.Db.Documents.SubDocuments;

namespace MongoDbDriverWrapper.Demo.Db.Documents
{
    public class Book : BaseDocument
    {
        public Book() { }
        public Book(string? id) : base(id) { }

        public Book(string? id,
            string authorId,
            string authorFullName,
            string title,
            string description,
            Publisher publisher) : base(id)
        {
            AuthorId = ObjectId.Parse(authorId);
            Title = title;
            Description = description;
            Publisher = publisher;
        }

        public ObjectId AuthorId { get; set; }
        public string AuthorFullName { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Version { get; set; }
        public Publisher? Publisher { get; set; }


    }
}
