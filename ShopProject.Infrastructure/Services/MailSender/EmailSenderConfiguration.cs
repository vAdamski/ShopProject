using Microsoft.Extensions.Configuration;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Common;

namespace ShopProject.Infrastructure.Services.MailSender;

public class EmailSenderConfiguration : IEmailSenderConfiguration
{
    public string From { get; private set; }
    public string SmtpServer { get; private set; }
    public int SmtpPort { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }

    public EmailSenderConfiguration(IConfiguration configuration)
    {
        From = configuration["EmailSender:From"];
        SmtpServer = configuration["EmailSender:SmtpServer"];
        SmtpPort = int.Parse(configuration["EmailSender:SmtpPort"]);
        Username = configuration["EmailSender:Username"];
        Password = configuration["EmailSender:Password"];
        
        Validate();
    }
    
    public EmailConfiguration GetEmailSenderConfiguration()
    {
        return new EmailConfiguration
        {
            From = From,
            SmtpServer = SmtpServer,
            SmtpPort = SmtpPort,
            Username = Username,
            Password = Password
        };
    }
    
    private void Validate()
    {
        if (string.IsNullOrEmpty(From))
            throw new ArgumentNullException(nameof(From));

        if (string.IsNullOrEmpty(SmtpServer))
            throw new ArgumentNullException(nameof(SmtpServer));

        if (SmtpPort == 0)
            throw new ArgumentNullException(nameof(SmtpPort));

        if (string.IsNullOrEmpty(Username))
            throw new ArgumentNullException(nameof(Username));

        if (string.IsNullOrEmpty(Password))
            throw new ArgumentNullException(nameof(Password));
    }
}