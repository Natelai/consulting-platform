using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Infrastructure.mongo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Consulting.Auth.Presentation.Endpoints
{
    [ApiController]
    [Route("/test-results")]
    public class TestResultsController : ControllerBase
    {
        private readonly ITestResultService _service;

        public TestResultsController(ITestResultService service)
        {
            _service = service;
        }

        [HttpPost("dreyfus")]
        public async Task<IActionResult> CreateDreyfus([FromBody] TestResult result)
        {
            await _service.SaveDreyfusResultAsync(result);
            return Ok();
        }

        [HttpPost("career")]
        public async Task<IActionResult> CreateCareer([FromBody] TestResult result)
        {
            await _service.SaveCareerResultAsync(result);
            return Ok();
        }

        [HttpPut("dreyfus/{userId}")]
        public async Task<IActionResult> UpdateDreyfus(string userId, [FromBody] TestResult result)
        {
            await _service.UpdateDreyfusResultAsync(userId, result);
            return Ok();
        }

        [HttpPut("career/{userId}")]
        public async Task<IActionResult> UpdateCareer(string userId, [FromBody] TestResult result)
        {
            await _service.UpdateCareerResultAsync(userId, result);
            return Ok();
        }

        // Інші ендпоінти для отримання даних
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var results = await _service.GetAllResultsAsync();
            return Ok(results);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var results = await _service.GetUserResultsAsync(userId);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetResultByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
