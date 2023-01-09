namespace MongoDbDriverWrapper.Demo.Db.Documents.SubDocuments
{
    public class Publisher
    {
        public Publisher() { }

        public Publisher(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
