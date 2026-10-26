namespace MyStore.Exceptions;

public class UsernameAlreadyTakenException : Exception
{
    public UsernameAlreadyTakenException(string username)
        : base($"Пользователь с таким именем '{username}' уже существует.")
    {
    }
}