namespace Consulting.Auth.Infrastructure.mongo.Repositories.Repository
{
    public interface IVacancyRepository
    {
        Task CreateAsync(Vacancy vacancy);
        Task<List<Vacancy>> GetAllAsync();
        Task<Vacancy?> GetByIdAsync(string id);
        Task UpdateAsync(Vacancy vacancy);
        Task DeleteAsync(string id);
    }
}