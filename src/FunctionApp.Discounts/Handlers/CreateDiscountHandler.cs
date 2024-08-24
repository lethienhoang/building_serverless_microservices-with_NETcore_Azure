using FunctionApp.Discounts.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, Guid>
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<CreateDiscountHandler> _logger;
    public CreateDiscountHandler(AppDbContext appDbContext, ILogger<CreateDiscountHandler> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }
    public async Task<Guid> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Discount is created. ProductName : {ProductName}", request.ProductName);
        var coupon = new Coupon(request.ProductName, request.ProductDescription, request.Amount);
        await _appDbContext.AddAsync(coupon);
        await _appDbContext.SaveChangesAsync();
        _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", request.ProductName);
        return coupon.Id;
    }
}