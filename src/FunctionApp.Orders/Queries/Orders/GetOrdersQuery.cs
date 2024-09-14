using FunctionApp.Orders.Dtos;
using MediatR;

namespace FunctionApp.Orders.Queries.Orders
{
    public record GetOrdersQuery(int pageNumber = 1, int pageSize = 10)
    : IRequest<GetOrdersResult>;

    public record GetOrdersResult(IEnumerable<OrderDto> Orders);
}