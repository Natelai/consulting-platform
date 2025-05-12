using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Infrastructure.mongo;
using Consulting.Auth.Infrastructure.mongo.Repositories.Repository;

namespace Consulting.Auth.Application
{
    public class VacancyService : IVacancyService
    {
        private readonly IVacancyRepository _repository;

        public VacancyService(IVacancyRepository repository)
        {
            _repository = repository;
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
    }
}