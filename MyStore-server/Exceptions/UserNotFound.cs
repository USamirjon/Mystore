namespace MyStore.Exceptions;

public class UserNotFound:Exception
{
    public UserNotFound(int userId)
        : base($"Пользователь с id {userId} не найден.")
    {
    }
}