namespace Consulting.Auth.Infrastructure.mongo;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
}