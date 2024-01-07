using ShopProject.Shared.Common;

namespace ShopProject.Application.Common.Interfaces;

public interface IEmailSenderConfiguration
{
    string From { get; }
    string SmtpServer { get; }
    int SmtpPort { get; }
    string Username { get; }
    string Password { get; }

    EmailConfiguration GetEmailSenderConfiguration();
}