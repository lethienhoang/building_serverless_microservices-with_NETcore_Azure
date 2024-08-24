
using FunctionApp.Discounts.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunctionApp.Discounts.Handlers;

public class GetDiscountHandler : IRequestHandler<GetDiscountQuery, List<CouponDto>>
{
    private readonly AppDbContext _appDbContext;
    public GetDiscountHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<List<CouponDto>> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        return await _appDbContext.Coupons
            .Select(item => new CouponDto(item.Id, item.ProductName, item.Description, item.Amount)).ToListAsync();
    }
}