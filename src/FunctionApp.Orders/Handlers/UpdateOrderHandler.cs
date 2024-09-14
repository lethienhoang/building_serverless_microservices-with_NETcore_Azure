using FunctionApp.Orders.Commands.Orders;
using FunctionApp.Orders.Dtos;
using FunctionApp.Orders.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FunctionApp.Orders.Handlers
{
    public class UpdateOrderHandler(AppDbContext dbContext)
     : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            //Update Order entity from command object
            //save to database
            //return result

            var orderId = command.Order.Id;
            var order = await dbContext.Orders
                .FindAsync([orderId], cancellationToken: cancellationToken);

            if (order is null)
            {
                //TODO
            }

            UpdateOrderWithNewValues(order, command.Order);

            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
        {
        }
    }
}