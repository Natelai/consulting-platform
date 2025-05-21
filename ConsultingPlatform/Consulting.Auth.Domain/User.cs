using Microsoft.AspNetCore.Identity;

namespace Consulting.Auth.Domain;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SecondName { get; set; }
    public DateOnly? BirthDate { get; set; }
}
