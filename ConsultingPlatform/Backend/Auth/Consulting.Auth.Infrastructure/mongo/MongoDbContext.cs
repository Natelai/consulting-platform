using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Consulting.Auth.Infrastructure.mongo;

public class MongoDbContext
{
    public IMongoDatabase Database { get; }

    public MongoDbContext(IConfiguration configuration)
    {
        var settings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
        var client = new MongoClient(settings.ConnectionString);
        Database = client.GetDatabase(settings.Database);
    }

    public IMongoCollection<TestResult> TestResults => Database.GetCollection<TestResult>("testResults");
    public IMongoCollection<Vacancy> Vacancies => Database.GetCollection<Vacancy>("vacancies");
}