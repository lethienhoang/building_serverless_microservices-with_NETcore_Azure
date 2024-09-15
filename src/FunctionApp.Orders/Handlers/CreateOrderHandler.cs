using FunctionApp.Orders.Commands.Orders;
using FunctionApp.Orders.Dtos;
using FunctionApp.Orders.Models;
using MediatR;


namespace FunctionApp.Orders.Handlers
{
    public class CreateOrderHandler(AppDbContext dbContext)
    : IRequestHandler<CreateOrderCommand, Commands.Orders.CreateOrderResult>
    {
        public async Task<Commands.Orders.CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            //create Order entity from command object
            //save to database
            //return result 

            var order = CreateNewOrder(command.Order);

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new Commands.Orders.CreateOrderResult(order.Id);
        }

        private Order CreateNewOrder(OrderDto orderDto)
        {
            var shippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

            var newOrder = Order.Create(
                    id: Guid.NewGuid(),
                    customerId: orderDto.CustomerId,
                    orderName: orderDto.OrderName,
                    shippingAddress: shippingAddress,
                    billingAddress: billingAddress,
                    payment: Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
                    );

            foreach (var orderItemDto in orderDto.OrderItems)
            {
                newOrder.Add(orderItemDto.ProductId, orderItemDto.Quantity, orderItemDto.Price);
            }
            return newOrder;
        }
    }
}