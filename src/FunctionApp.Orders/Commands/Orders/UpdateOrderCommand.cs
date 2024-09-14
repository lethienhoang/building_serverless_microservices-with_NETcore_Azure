using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunctionApp.Orders.Dtos;

namespace FunctionApp.Orders.Commands.Orders
{
    public record UpdateOrderCommand(OrderDto Order)
    : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccess);

}