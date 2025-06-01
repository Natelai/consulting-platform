using Consulting.Auth.Domain;
using Consulting.Auth.Infrastructure.db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Consulting.Auth.Presentation.Endpoints;

[ApiController]
[Route("profile")]
public class UserProfileController(AppDbContext context, UserManager<User> userManager) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly UserManager<User> _userManager = userManager;

    [HttpGet]
    public async Task<IActionResult> GetMyProfileAsync()
    {
        var email = User?.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest();
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user is null)
        {
            return BadRequest();
        }

        return Ok(new
        {
            user.FirstName,
            user.LastName,
            user.SecondName,
            user.BirthDate
        });
    }

    [HttpPut("firstName")]
    public async Task<IActionResult> ChangeFirstNameAsync([FromBody] string firstName)
    {
        var email = User?.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest();
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return BadRequest();
        }

        user.FirstName = firstName;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("lastName")]
    public async Task<IActionResult> ChangeLastNameAsync([FromBody] string lastName)
    {
        var email = User?.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest();
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return BadRequest();
        }

        user.LastName = lastName;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("secondName")]
    public async Task<IActionResult> ChangeFirstName([FromBody] string secondName)
    {
        var email = User?.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest();
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return BadRequest();
        }

        user.SecondName = secondName;
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("birth")]
    public async Task<IActionResult> ChangeFirstName([FromBody] DateOnly birth)
    {
        var email = User?.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest();
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return BadRequest();
        }

        user.BirthDate = birth;
        await _context.SaveChangesAsync();

        return Ok();
    }   
}
