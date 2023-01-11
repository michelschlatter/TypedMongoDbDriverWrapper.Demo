namespace MongoDbDriverWrapper.Demo.Db.Documents
{
    public class Author : BaseDocument
    {
        public Author() { }
        public Author(string? id) : base(id) { }

        public Author(string? id,
            string fullName,
            int yearBorn,
            string address,
            string telephone) : base(id)
        {
            FullName = fullName;
            YearBorn = yearBorn;
            Address = address;
            Telephone = telephone;
        }

        public string FullName { get; set; } = string.Empty;
        public int YearBorn { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
    }
}
