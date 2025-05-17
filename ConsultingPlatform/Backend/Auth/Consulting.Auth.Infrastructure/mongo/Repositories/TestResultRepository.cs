using Consulting.Auth.Infrastructure.mongo;
using Consulting.Auth.Infrastructure.mongo.Repositories.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Consulting.Auth.Infrastructure.mongo.Repositories
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly IMongoCollection<TestResult> _collection;

        public TestResultRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.Database);
            _collection = database.GetCollection<TestResult>("testResults");
        }

        public async Task CreateAsync(TestResult result) =>
            await _collection.InsertOneAsync(result);

        public async Task UpdateDreyfusScoreAsync(string userId, int dreyfusScore, Dictionary<string, int> scoresByBlock)
        {
            var filter = Builders<TestResult>.Filter.Eq(x => x.UserId, userId);
            var update = Builders<TestResult>.Update
                .Set(x => x.DreyfusScore, dreyfusScore)
                .Set(x => x.ScoresByBlock, scoresByBlock);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateCareerTraitsAsync(string userId, List<TraitScore> careerTraits)
        {
            var filter = Builders<TestResult>.Filter.Eq(x => x.UserId, userId);
            var update = Builders<TestResult>.Update.Set(x => x.CareerTraits, careerTraits);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<List<TestResult>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<TestResult?> GetByUserIdAsync(string userId) =>
            await _collection.Find(x => x.UserId == userId).FirstOrDefaultAsync();

        public async Task<TestResult?> GetByIdAsync(string id) =>
            await _collection.Find(x => x.Id == MongoDB.Bson.ObjectId.Parse(id)).FirstOrDefaultAsync();
    }
}
