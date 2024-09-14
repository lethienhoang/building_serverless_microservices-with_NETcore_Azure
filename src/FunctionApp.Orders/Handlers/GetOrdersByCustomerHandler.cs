using FunctionApp.Orders.Queries.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunctionApp.Orders.Handlers
{
    public class GetOrdersByCustomerHandler(AppDbContext dbContext)
    : IRequestHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            // get orders by customer using dbContext
            // return result

            var orders = await dbContext.Orders
                            .Include(o => o.OrderItems)
            .AsNoTracking()
                            .Where(o => o.CustomerId == query.CustomerId)
                            .OrderBy(o => o.OrderName)
                            .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.Select(item => new Dtos.OrderDto
            {

            }));
        }
    }
}