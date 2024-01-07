using MailKit.Net.Smtp;
using MimeKit;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Common;

namespace ShopProject.Infrastructure.Services.MailSender;

public class MailSenderService : IMailSenderService
{
    private readonly EmailConfiguration _emailConfiguration;

    public MailSenderService(IEmailSenderConfiguration emailSenderConfiguration)
    {
        _emailConfiguration = emailSenderConfiguration.GetEmailSenderConfiguration();
    }
    
    public async Task SendMail(string toEmail, string subject, string body)
    {
        MimeMessage email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailConfiguration.From, _emailConfiguration.From));
        email.To.Add(new MailboxAddress(toEmail, toEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = body
        };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfiguration.Username, _emailConfiguration.Password);
            await client.SendAsync(email);

            await client.DisconnectAsync(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}