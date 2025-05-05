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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TestResult result)
        {
            await _service.SaveResultAsync(result);
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] TestResult result)
        {
            await _service.UpdateResultAsync(userId, result);
            return Ok();
        }

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
