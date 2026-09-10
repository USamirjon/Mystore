namespace MyStore.Exceptions;

public class EmailAlreadyTakenException : Exception
{
    public EmailAlreadyTakenException(string email)
        : base($"Пользователь с таким email '{email}' уже существует.")
    {
    }
}