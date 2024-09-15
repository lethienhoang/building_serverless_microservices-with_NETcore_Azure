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

            return new GetOrdersResult(orders.Select(order => new OrderDto
            (
                order.Id,
                order.CustomerId,
                order.OrderName,
                new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode),
                new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                order.Status,
                order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList()
            )));
        }
    }
}