using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Common.Services;

public interface ICodesMailSenderService
{
    Task SendMail(CodesMailRequest codesMailRequest);
}