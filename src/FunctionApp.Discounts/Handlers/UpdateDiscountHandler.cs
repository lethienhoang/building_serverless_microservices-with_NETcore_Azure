
using FunctionApp.Discounts.Commands;
using MediatR;

public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, CouponDto>
{
    public UpdateDiscountHandler()
    {
        
    }
    public Task<CouponDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}