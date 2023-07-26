using System.ComponentModel.DataAnnotations;

namespace backend.Application.Dtos;

public record CheckAmountDto
{
    [Required(ErrorMessage = "Нужно обязательно подать сумму чека.")]
    [Range(1, int.MaxValue)]
    public decimal Value { get; init; }
}