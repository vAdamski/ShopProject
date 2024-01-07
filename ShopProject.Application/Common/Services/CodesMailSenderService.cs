using ShopProject.Application.Common.Builders;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Common.Services;

public class CodesMailSenderService : ICodesMailSenderService
{
    private readonly IMailSenderService _mailSenderService;

    public CodesMailSenderService(IMailSenderService mailSenderService)
    {
        _mailSenderService = mailSenderService;
    }

    public async Task SendMail(CodesMailRequest codesMailRequest)
    {
        var body = new MailBodyBuilder(codesMailRequest.Games)
            .WithOrderId(codesMailRequest.OrderId)
            .Build();

        await _mailSenderService.SendMail(codesMailRequest.CustomerEmail, $"Twoje kody - {codesMailRequest.OrderId}",
            body);
    }
}