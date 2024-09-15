using FunctionApp.Orders.Dtos;
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

            return new GetOrdersByCustomerResult(orders.Select(order => new OrderDto
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