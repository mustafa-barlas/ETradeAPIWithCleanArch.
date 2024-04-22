namespace ETradeAPI.Application.Abstractions.Services;

public interface IMailService
{
    Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);

    Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);

    Task SendPasswordResetMailAsync(string to, string userId, string resetToken);

    Task SendCompletedOrderMailAsync(string to, string Name, string userSurname, string orderCode, DateTime orderDate);
}