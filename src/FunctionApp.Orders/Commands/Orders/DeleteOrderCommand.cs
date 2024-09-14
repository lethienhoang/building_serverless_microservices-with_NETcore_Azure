using MediatR;

namespace FunctionApp.Orders.Commands.Orders
{
    public record DeleteOrderCommand(Guid OrderId)
    : IRequest<DeleteOrderResult>;

    public record DeleteOrderResult(bool IsSuccess);
}