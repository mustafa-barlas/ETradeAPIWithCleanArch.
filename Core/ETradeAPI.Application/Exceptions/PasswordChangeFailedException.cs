namespace ETradeAPI.Application.Exceptions;

public class PasswordChangeFailedException : Exception
{
    public PasswordChangeFailedException() : base("Şifre güncelenirken bir sorun oluştu")
    {

    }

    public PasswordChangeFailedException(string? message) : base(message)
    {

    }

    public PasswordChangeFailedException(string? message, Exception? innerException) : base(message, innerException)
    {

    }
}