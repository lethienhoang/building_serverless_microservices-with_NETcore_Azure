
using System.Data.SqlTypes;
using FunctionApp.Discounts.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, CouponDto>
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<UpdateDiscountHandler> _logger;
    public UpdateDiscountHandler(AppDbContext appDbContext, ILogger<UpdateDiscountHandler> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }
    public async Task<CouponDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = _appDbContext.Coupons.FirstOrDefault(x => x.ProductName == request.ProductName);
        if (coupon is null)
        {
            throw new SqlNullValueException($"Discount with ProductName={request.ProductName} is not found.");
        }

        coupon.Amount = request.Amount;
        coupon.Description = request.ProductDescription;

        _appDbContext.Coupons.Update(coupon);
        await _appDbContext.SaveChangesAsync();

        _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

        return new CouponDto(coupon.Id, coupon.ProductName, coupon.Description, coupon.Amount);
    }
}