using MediatR;
using ShopProject.Shared.Dtos;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Application.Payments.Commands.CreatePayment;

public class CreatePaymentCommand : IRequest<CreatePaymentStatus>
{
    public CartPaymentVm CartPaymentVm { get; set; }
}