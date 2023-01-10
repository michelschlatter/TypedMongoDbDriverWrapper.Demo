using MongoDbDriverWrapper.Demo.Api.Converters;
using MongoDbDriverWrapper.Demo.Db.DbContext;
using MongoDbDriverWrapper.Demo.Db.Documents;
using MongoDbDriverWrapper.Demo.Db.Documents.SubDocuments;
using MongoDbDriverWrapper.Demo.Db.Repositories;
using TypedMongoDbDriverWrapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    //Add Custom Converter for ObjectId (handle as string) (@Typed.MongoDb.Driver.Wrapper)
    options.JsonSerializerOptions.Converters.Add(new ObjectIdJsonConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register Dependency Injection (@Typed.MongoDb.Driver.Wrapper)
var config = builder.Configuration;
var dbContext = await DbContextFactory.CreateAsync(
    config.GetValue<string>("DbName") ?? throw new Exception($"Add 'DbName' to application.{builder.Environment.EnvironmentName}.json"),
    config.GetConnectionString("MongoDb") ?? throw new Exception($"Add 'MongoDb' to ConnectionStrings in application.{builder.Environment.EnvironmentName}.json"));

builder.Services.AddSingleton<IDbContext, AppDbContext>(_ => dbContext);
builder.Services.AddSingleton<AuthorRepository>();
builder.Services.AddSingleton<BookRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Fill in some TestData in the MongoDb (@Typed.MongoDb.Driver.Wrapper)
var authorRepository = app.Services.GetRequiredService<AuthorRepository>();
var bookRepository = app.Services.GetRequiredService<BookRepository>();

var author = new Author(null, "Patrick Süsskind",
    1949,
    "Ambach am Starnberger See",
    "+49 89 123456789");
await authorRepository.InsertOneAsync(author);

var book = new Book(null, author.Id.ToString(),
    author.FullName,
    "Das Parfüm",
    "..Description..",
    new Publisher("Diogenes Verlag", "Zürich"));
await bookRepository.InsertOneAsync(book);


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
