using Consulting.Auth.Infrastructure.mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consulting.Auth.Application.Abstractions
{
    public interface ITestResultService
    {
        Task SaveDreyfusResultAsync(TestResult result);
        Task SaveCareerResultAsync(TestResult result);
        Task UpdateDreyfusResultAsync(string userId, TestResult updatedResult);
        Task UpdateCareerResultAsync(string userId, TestResult updatedResult);
        Task<List<TestResult>> GetAllResultsAsync();
        Task<TestResult?> GetUserResultsAsync(string userId);
        Task<TestResult?> GetResultByIdAsync(string id);
    }
}
