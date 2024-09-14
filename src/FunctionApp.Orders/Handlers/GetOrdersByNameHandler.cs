using FunctionApp.Orders.Queries.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunctionApp.Orders.Handlers
{
    public class GetOrdersByNameHandler(AppDbContext dbContext)
    : IRequestHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
        {
            // get orders by name using dbContext
            // return result

            var orders = await dbContext.Orders
                    .Include(o => o.OrderItems)
                    .AsNoTracking()
                    .Where(o => o.OrderName.Contains(query.Name))
                    .OrderBy(o => o.OrderName)
                    .ToListAsync(cancellationToken);

            return new GetOrdersByNameResult(orders.Select(item => new Dtos.OrderDto
            {

            }));
        }
    }
}