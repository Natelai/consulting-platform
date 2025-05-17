using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Infrastructure.mongo;
using Consulting.Auth.Infrastructure.mongo.Repositories;
using Consulting.Auth.Infrastructure.mongo.Repositories.Repository;

namespace Consulting.Auth.Application
{
    public class VacancyMatchResult
    {
        public Vacancy Vacancy { get; set; } = null!;
        public double MatchPercentage { get; set; }
    }
    
    public class VacancyService : IVacancyService
    {
        private readonly IVacancyRepository _repository;
        private readonly ITestResultRepository _testResultRepository;

        public VacancyService(IVacancyRepository repository, ITestResultRepository testResultRepository)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
        }

        public async Task CreateVacancyAsync(Vacancy vacancy) =>
            await _repository.CreateAsync(vacancy);

        public async Task<List<Vacancy>> GetAllVacanciesAsync() =>
            await _repository.GetAllAsync();

        public async Task<Vacancy?> GetVacancyByIdAsync(string id) =>
            await _repository.GetByIdAsync(id);

        public async Task UpdateVacancyAsync(Vacancy vacancy) =>
            await _repository.UpdateAsync(vacancy);

        public async Task DeleteVacancyAsync(string id) =>
            await _repository.DeleteAsync(id);

        public async Task<List<VacancyMatchResult>> GetSortedVacanciesForUserAsync(string userId)
        {
            var testResult = await _testResultRepository.GetByUserIdAsync(userId);
            if (testResult == null || testResult.CareerTraits == null || testResult.DreyfusScore == 0 || !testResult.CareerTraits.Any())
                throw new InvalidOperationException("Тести не пройдено");

            var vacancies = await _repository.GetAllAsync();
            var result = new List<VacancyMatchResult>();

            foreach (var vacancy in vacancies)
            {
                double match = 0;

                // Порівняння DreyfusScore
                var dreyfusDiff = Math.Abs(testResult.DreyfusScore - vacancy.RequiredDreyfusScore);
                double dreyfusScoreMatch = dreyfusDiff > 5 ? 0 : 1 - dreyfusDiff / 5.0;

                // Порівняння CareerTraits
                var matchingTraits = 0.0;
                foreach (var required in vacancy.RequiredTraits)
                {
                    var userTrait = testResult.CareerTraits.FirstOrDefault(t => t.Trait == required.Trait);
                    if (userTrait != null)
                    {
                        var diff = Math.Abs(required.Score - userTrait.Score);
                        matchingTraits += diff > 1 ? 0 : 1 - diff; // 0–1 для кожної риси
                    }
                }

                double traitMatch = matchingTraits / vacancy.RequiredTraits.Count;

                match = (dreyfusScoreMatch * 0.4) + (traitMatch * 0.6); // можна змінити ваги

                result.Add(new VacancyMatchResult
                {
                    Vacancy = vacancy,
                    MatchPercentage = Math.Round(match * 100, 2)
                });
            }

            return result.OrderByDescending(v => v.MatchPercentage).ToList();
        }
    }
}