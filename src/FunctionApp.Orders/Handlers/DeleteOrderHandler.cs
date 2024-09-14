using FunctionApp.Orders.Commands.Orders;
using FunctionApp.Orders.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionApp.Orders.Handlers
{
    public class DeleteOrderHandler(AppDbContext dbContext)
    : IRequestHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            //Delete Order entity from command object
            //save to database
            //return result

            var orderId = command.OrderId;
            var order = await dbContext.Orders
                .FindAsync([orderId], cancellationToken: cancellationToken);

            if (order is null)
            {
                //TODO
            }

            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}