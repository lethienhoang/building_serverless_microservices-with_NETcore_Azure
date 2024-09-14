using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunctionApp.Orders.Dtos;
using MediatR;

namespace FunctionApp.Orders.Commands.Orders
{
    public record UpdateOrderCommand(OrderDto Order)
    : IRequest<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccess);

}