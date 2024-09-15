using FunctionApp.Orders.Dtos;
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

            return new GetOrdersByNameResult(orders.Select(order => new Dtos.OrderDto
            (
                order.Id,
                order.CustomerId,
                order.OrderName,
                new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
                new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode),
                new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
                order.Status,
                order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList())));
        }
    }
}