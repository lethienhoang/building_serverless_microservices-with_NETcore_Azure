using FunctionApp.Catalogs.Dtos;
using FunctionApp.Catalogs.Queries.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunctionApp.Catalogs.Handlers.Products
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly AppDbContext _appDbContext;
        public GetProductsHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _appDbContext.Products.Skip(request.pageNumber).Take(request.pageSize).ToArrayAsync();
            if (!products.Any())
            {
                throw new ArgumentException("Data is null");
            }

            return products.Select(product => new ProductDto(product.Id, product.Name, product.Description, product.Price)).ToList();
        }
    }
}