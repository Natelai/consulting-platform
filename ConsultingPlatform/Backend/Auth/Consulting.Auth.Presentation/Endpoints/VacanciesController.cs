using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Infrastructure.mongo;
using Microsoft.AspNetCore.Mvc;

namespace Consulting.Auth.Presentation.Endpoints
{
    [ApiController]
    [Route("vacancies")]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacancyService _service;

        public VacanciesController(IVacancyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vacancy vacancy)
        {
            await _service.CreateVacancyAsync(vacancy);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vacancies = await _service.GetAllVacanciesAsync();
            return Ok(vacancies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var vacancy = await _service.GetVacancyByIdAsync(id);
            if (vacancy == null) return NotFound();
            return Ok(vacancy);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Vacancy vacancy)
        {
            vacancy.Id = MongoDB.Bson.ObjectId.Parse(id);
            await _service.UpdateVacancyAsync(vacancy);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteVacancyAsync(id);
            return Ok();
        }

        [HttpGet("matched/{userId}")]
        public async Task<IActionResult> GetMatchedVacancies(string userId)
        {
            var results = await _service.GetSortedVacanciesForUserAsync(userId);
            return Ok(results);
        }

    }
}