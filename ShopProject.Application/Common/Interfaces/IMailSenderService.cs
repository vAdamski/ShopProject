namespace ShopProject.Application.Common.Interfaces;

public interface IMailSenderService
{
    Task SendMail(string toEmail, string subject, string body);
}