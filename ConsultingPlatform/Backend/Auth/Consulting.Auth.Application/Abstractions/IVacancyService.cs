using Consulting.Auth.Infrastructure.mongo;

namespace Consulting.Auth.Application.Abstractions
{
    public interface IVacancyService
    {
        Task CreateVacancyAsync(Vacancy vacancy);
        Task<List<Vacancy>> GetAllVacanciesAsync();
        Task<Vacancy?> GetVacancyByIdAsync(string id);
        Task UpdateVacancyAsync(Vacancy vacancy);
        Task DeleteVacancyAsync(string id);
    }
}