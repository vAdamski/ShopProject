using MediatR;
using ShopProject.Shared.Dtos;

namespace ShopProject.Application.Payments.Commands.CreateRepaymentRequest;

public class CreateRepaymentRequestCommand : IRequest<CreatePaymentStatus>
{
    public Guid OrderId { get; set; }
}