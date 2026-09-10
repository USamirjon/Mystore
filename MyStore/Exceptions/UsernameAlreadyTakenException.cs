namespace MyStore.Exceptions;

public class UsernameAlreadyTakenException : Exception
{
    public UsernameAlreadyTakenException(string username)
        : base($"Пользователь с таким username '{username}' уже существует.")
    {
    }
}