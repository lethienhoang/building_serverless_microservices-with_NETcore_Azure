
using FunctionApp.Discounts.Commands;
using MediatR;

namespace FunctionApp.Discounts.Handlers;

public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    public DeleteDiscountHandler()
    {
        
    }
    public Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}