using FunctionApp.Catalogs.Dtos;
using MediatR;

namespace FunctionApp.Catalogs.Queries.Products
{
    public record GetProductsQuery(int pageNumber = 1, int pageSize = 10) : IRequest<List<ProductDto>>;
}