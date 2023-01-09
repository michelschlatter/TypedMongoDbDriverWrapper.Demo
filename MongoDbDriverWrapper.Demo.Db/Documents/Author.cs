﻿namespace MongoDbDriverWrapper.Demo.Db.Documents
{
    public class Author : BaseDocument
    {
        public Author() { }
        public Author(string? id) : base(id) { }

        public Author(string? id, string name, int yearBorn)
        {
            Name = name;
            YearBorn = yearBorn;
        }

        public string Name { get; set; } = string.Empty;
        public int YearBorn { get; set; }
    }
}
