using FunctionApp.Orders.Dtos;
using FunctionApp.Orders.Queries.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunctionApp.Orders.Handlers
{
    public class GetOrdersHandler(AppDbContext dbContext)
    : IRequestHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            // get orders with pagination
            // return result

            var pageNumber = query.pageNumber;
            var pageSize = query.pageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orders = await dbContext.Orders
                           .Include(o => o.OrderItems)
                           .OrderBy(o => o.OrderName)
                           .Skip(pageSize * pageNumber)
                           .Take(pageSize)
                           .ToListAsync(cancellationToken);

            return new GetOrdersResult(orders.Select(item => new OrderDto
            {

            }));
        }
    }
}