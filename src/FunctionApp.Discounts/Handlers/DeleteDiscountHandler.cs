
using System.Data.SqlTypes;
using FunctionApp.Discounts.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Discounts.Handlers;

public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<CreateDiscountHandler> _logger;
    public DeleteDiscountHandler(AppDbContext appDbContext, ILogger<CreateDiscountHandler> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }
    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var coupon = _appDbContext.Coupons.FirstOrDefault(x => x.ProductName == request.ProductName);
        if (coupon is null)
        {
            throw new SqlNullValueException($"Discount with ProductName={request.ProductName} is not found.");
        }
        _logger.LogInformation("Discount is exist in database. ProductName : {ProductName}", request.ProductName);

        _appDbContext.Coupons.Remove(coupon);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}