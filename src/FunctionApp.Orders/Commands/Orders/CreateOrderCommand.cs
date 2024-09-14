using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunctionApp.Orders.Dtos;

namespace FunctionApp.Orders.Commands.Orders
{
    public record CreateOrderCommand(OrderDto Order)
      : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);
}