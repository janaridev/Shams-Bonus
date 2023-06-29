namespace backend.Domain.Exceptions.NotFound;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string phoneNumber) :
        base($"Клиента с номером: {phoneNumber} не существует в базе данных")
    { }
}