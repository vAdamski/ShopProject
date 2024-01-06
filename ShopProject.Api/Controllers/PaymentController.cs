using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Common.Interfaces;
using ShopProject.Application.Payments.Commands.CreatePayment;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Api.Controllers;

[Route("api/[controller]")]
public class PaymentController : BaseController
{
    [HttpPost("create-checkout-session")]
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
}