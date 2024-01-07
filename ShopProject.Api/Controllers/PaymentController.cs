using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Payments.Commands.CreatePayment;
using ShopProject.Application.Payments.Commands.CreateRepaymentRequest;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Api.Controllers;

[Route("api/[controller]")]
public class PaymentController : BaseController
{
    
    [HttpPost("create-checkout-session")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCheckoutSession(CartPaymentVm vm)
    {
        var response = await Mediator.Send(new CreatePaymentCommand()
        {
            CartPaymentVm = vm
        });
        
        if (!response.Success)
            return BadRequest(response.ErrorMessage);
        
        return Ok(response.Uri);
    }

    [HttpPost("create-checkout-session/{orderId}")]
    [Authorize]
    public async Task<IActionResult> CreateRepaymentSession(Guid orderId)
    {
        var response = await Mediator.Send(new CreateRepaymentRequestCommand
        {
            OrderId = orderId
        });
        
        if (!response.Success)
            return BadRequest(response.ErrorMessage);
        
        return Ok(response.Uri);
    }
}