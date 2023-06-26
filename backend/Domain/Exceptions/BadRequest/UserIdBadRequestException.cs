namespace backend.Domain.Exceptions.BadRequest;

public sealed class UserIdBadRequestException : BadRequestException
{
    public UserIdBadRequestException() :
        base("Номер телефона который вы ввели является пустым.")
    { }
}