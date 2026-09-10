namespace MyStore.Exceptions;

public class InvalidCredentialsException : Exception
{
    public InvalidCredentialsException()
        : base("Неверный логин или пароль.")
    {
    }
}