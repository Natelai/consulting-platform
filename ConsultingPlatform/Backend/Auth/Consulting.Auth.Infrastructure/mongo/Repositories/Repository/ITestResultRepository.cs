namespace Consulting.Auth.Infrastructure.mongo.Repositories.Repository
{
    public interface ITestResultRepository
    {
        Task CreateAsync(TestResult result);
        Task<List<TestResult>> GetAllAsync();
        Task<TestResult?> GetByUserIdAsync(string userId);
        Task<TestResult?> GetByIdAsync(string id);
        Task UpdateCareerTraitsAsync(string userId, List<TraitScore> careerTraits);
        Task UpdateDreyfusScoreAsync(string userId, int dreyfusScore);
    }
}
