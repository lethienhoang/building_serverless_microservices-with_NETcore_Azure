using FunctionApp.Catalogs.Dtos;
using MediatR;

namespace FunctionApp.Catalogs.Queries.Products
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
}