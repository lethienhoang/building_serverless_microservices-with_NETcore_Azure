using FunctionApp.Orders.Commands.Orders;
using FunctionApp.Orders.Dtos;
using FunctionApp.Orders.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionApp.Orders.Handlers
{
    public class CreateOrderHandler(AppDbContext dbContext)
    : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            //create Order entity from command object
            //save to database
            //return result 

            var order = CreateNewOrder(command.Order);

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
           
        }
    }
}