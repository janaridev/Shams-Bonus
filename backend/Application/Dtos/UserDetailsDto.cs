namespace backend.Application.Dtos;

public record UserDetailsDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string UserName { get; init; }
    public decimal Bonuses { get; set; }
}