using Consulting.Auth.Infrastructure.mongo;
using Consulting.Auth.Infrastructure.mongo.Repositories.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Consulting.Auth.Infrastructure.mongo.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly IMongoCollection<Vacancy> _collection;

        public VacancyRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.Database);
            _collection = database.GetCollection<Vacancy>("vacancies");
        }

        public async Task CreateAsync(Vacancy vacancy) =>
            await _collection.InsertOneAsync(vacancy);

        public async Task<List<Vacancy>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Vacancy?> GetByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            return await _collection.Find(x => x.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Vacancy vacancy)
        {
            var filter = Builders<Vacancy>.Filter.Eq(x => x.Id, vacancy.Id);
            await _collection.ReplaceOneAsync(filter, vacancy);
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            await _collection.DeleteOneAsync(x => x.Id == objectId);
        }
    }
}