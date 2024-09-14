using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunctionApp.Orders.Dtos;
using MediatR;

namespace FunctionApp.Orders.Commands.Orders
{
    public record CreateOrderCommand(OrderDto Order)
      : IRequest<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);
}