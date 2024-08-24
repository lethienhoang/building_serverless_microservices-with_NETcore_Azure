
using MediatR;

namespace FunctionApp.Discounts.Commands;

public record UpdateDiscountCommand(Guid Id, string ProductName, string ProductDescription, int Amount) : IRequest<CouponDto>;