using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunctionApp.Orders.Dtos;

namespace FunctionApp.Orders.Queries.Orders
{
    public record GetOrdersByNameQuery(string Name)
    : IQuery<GetOrdersByNameResult>;

    public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);
}