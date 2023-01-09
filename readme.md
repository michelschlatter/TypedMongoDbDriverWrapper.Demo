
# Typed.MongoDb.Driver.Wrapper Demo

Typed.MongoDb.Driver.Wrapper is a typed wrapper around the official MongoDbDriver and provides a BaseRepository which covers the basic CRUD operations.

This Project demonstrates the usage in a simple WebApi.  
It also shows how to
- Create ```Projections```
- Create ```Aggregation-Pipeline``` (join, match, sort)
- ```Update``` documents in different ways
- ```Delete``` documents
- etc.

If you wish for a specific example don't hesitate to contact me.

# Usage

The easiest way to start is to create a Free DB on MongoDb Atlas (https://www.mongodb.com/cloud/atlas/register) or
run the image ```mongo:latest``` in Docker.


In the  ``` application.development.json ``` you can add your ```connection string``` and ```dbname```.  
If you're using MongoDb Atlas do not forget to whitelist your IP in MongoDb Atlas else you will get a timeout.

Start the  ```Demo.Api``` Project and use the Swagger-Page to use the Api.


# Customize

Customize the following Classes

 - ``` CollectionProvider ```
 - ```AppDbContext```
 - ``` IndexFactory ``` (optional)
 - Add your own ``` Documents ``` and ``` Repositories ```



Every ``` Document ``` inherits ``` BaseDocument ``` which holds the ``` Id ```.  
Each ``` Repository ``` inherits from ``` BaseRepository ``` which implements basic CRUD-Operations.

#Appendix

See https://github.com/michelschlatter/TypedMongoDbDriverWrapper   
Nuget: ```dotnet add package Typed.MongoDb.Driver.Wrapper```


