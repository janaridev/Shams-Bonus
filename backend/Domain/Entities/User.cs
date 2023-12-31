using Microsoft.AspNetCore.Identity;

namespace backend.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Bonuses { get; set; }
}