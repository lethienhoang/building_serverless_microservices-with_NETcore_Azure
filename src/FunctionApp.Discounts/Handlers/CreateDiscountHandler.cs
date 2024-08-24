using FunctionApp.Discounts.Commands;
using MediatR;

public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, Guid>
{
    public CreateDiscountHandler()
    {
        
    }
    public Task<Guid> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}