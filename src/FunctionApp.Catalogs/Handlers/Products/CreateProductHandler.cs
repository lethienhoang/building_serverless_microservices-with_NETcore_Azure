using FunctionApp.Catalogs.Commands.Products;
using FunctionApp.Catalogs.Dtos;
using MediatR;

namespace FunctionApp.Catalogs.Handlers.Products;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductDto>
{
    public Task<CreateProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}