using FunctionApp.Orders.Dtos;
using MediatR;

namespace FunctionApp.Orders.Queries.Orders
{
    public record GetOrdersByCustomerQuery(Guid CustomerId)
    : IRequest<GetOrdersByCustomerResult>;

    public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);
}