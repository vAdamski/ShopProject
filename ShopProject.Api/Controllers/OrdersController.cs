using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Application.Orders.Commands.UpdateOrderState;
using ShopProject.Application.Orders.Queries.GetAllUserOrders;
using ShopProject.Application.Orders.Queries.GetAllUserOrdersByEmail;
using ShopProject.Shared.Enums;

namespace ShopProject.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class OrdersController : BaseController
{
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    [Route("get-orders")]
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            var orders = await Mediator.Send(new GetAllUserOrdersQuery());

            return Ok(orders);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting orders");
            return BadRequest();
        }
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("get-orders-by-email")]
    public async Task<IActionResult> GetOrders([FromQuery]string email)
    {
        try
        {
            var orders = await Mediator.Send(new GetAllUserOrdersByEmailQuery(email));

            return Ok(orders);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting orders");
            return BadRequest();
        }
    }
    
    [HttpGet("{orderId}/state/{state}")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateOrderState(Guid orderId, OrderState state)
    {
        try
        {
            await Mediator.Send(new UpdateOrderStateCommand
            {
                OrderId = orderId,
                State = state
            });

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating order state");
            return BadRequest();
        }
    }
}