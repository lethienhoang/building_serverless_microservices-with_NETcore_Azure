
using MediatR;

namespace FunctionApp.Discounts.Queries;

public record GetDiscountQuery : IRequest<List<CouponDto>>;