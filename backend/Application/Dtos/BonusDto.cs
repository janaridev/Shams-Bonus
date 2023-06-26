using System.ComponentModel.DataAnnotations;

namespace backend.Application.Dtos;

public record BonusDto
{
    [Required(ErrorMessage = "Нужно обязательно подать бонусы.")]
    [Range(1, int.MaxValue)]
    public decimal Value { get; init; }
}