using MediatR;
using ShopProject.Shared.ViewModels;

namespace ShopProject.Application.Orders.Queries.GetAllUserOrdersByEmail;

public class GetAllUserOrdersByEmailQuery : IRequest<OrdersListVm>
{
    public string Email { get; }

    public GetAllUserOrdersByEmailQuery(string email)
    {
        Email = email;
    }
}