using FunctionApp.Orders.Dtos;
using MediatR;

namespace FunctionApp.Orders.Queries.Orders
{
    public record GetOrdersByNameQuery(string Name)
    : IRequest<GetOrdersByNameResult>;

    public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
}