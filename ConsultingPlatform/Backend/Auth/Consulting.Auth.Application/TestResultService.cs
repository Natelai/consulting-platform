using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Infrastructure.mongo.Repositories.Repository;
using Consulting.Auth.Infrastructure.mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consulting.Auth.Application
{
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository _repository;

        public TestResultService(ITestResultRepository repository)
        {
            _repository = repository;
        }

        public async Task SaveDreyfusResultAsync(TestResult result)
        {
            var existing = await _repository.GetByUserIdAsync(result.UserId);
            if (existing != null)
            {
                result.Id = existing.Id;
                await _repository.UpdateDreyfusScoreAsync(result.UserId, result.DreyfusScore, result.ScoresByBlock);
            }
            else
            {
                await _repository.CreateAsync(result);
            }
        }

        public async Task SaveCareerResultAsync(TestResult result)
        {
            var existing = await _repository.GetByUserIdAsync(result.UserId);
            if (existing != null)
            {
                result.Id = existing.Id;
                await _repository.UpdateCareerTraitsAsync(result.UserId, result.CareerTraits);
            }
            else
            {
                await _repository.CreateAsync(result);
            }
        }

        public async Task UpdateDreyfusResultAsync(string userId, TestResult updatedResult) =>
            await _repository.UpdateDreyfusScoreAsync(userId, updatedResult.DreyfusScore, updatedResult.ScoresByBlock);

        public async Task UpdateCareerResultAsync(string userId, TestResult updatedResult) =>
            await _repository.UpdateCareerTraitsAsync(userId, updatedResult.CareerTraits);

        public async Task<List<TestResult>> GetAllResultsAsync() =>
            await _repository.GetAllAsync();

        public async Task<TestResult?> GetUserResultsAsync(string userId) =>
            await _repository.GetByUserIdAsync(userId);

        public async Task<TestResult?> GetResultByIdAsync(string id) =>
            await _repository.GetByIdAsync(id);
    }
}
