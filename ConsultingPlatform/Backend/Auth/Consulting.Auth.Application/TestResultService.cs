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

        public async Task SaveResultAsync(TestResult result)
        {
            var existing = await _repository.GetByUserIdAsync(result.UserId);
            if (existing != null)
            {
                result.Id = existing.Id; // зберігаємо той самий ObjectId
                await _repository.UpdateAsync(result.UserId, result);
            }
            else
            {
                await _repository.CreateAsync(result);
            }
        }

        public async Task UpdateResultAsync(string userId, TestResult updatedResult) =>
            await _repository.UpdateAsync(userId, updatedResult);

        public async Task<List<TestResult>> GetAllResultsAsync() =>
            await _repository.GetAllAsync();

        public async Task<TestResult?> GetUserResultsAsync(string userId) =>
            await _repository.GetByUserIdAsync(userId);

        public async Task<TestResult?> GetResultByIdAsync(string id) =>
            await _repository.GetByIdAsync(id);
    }
}
