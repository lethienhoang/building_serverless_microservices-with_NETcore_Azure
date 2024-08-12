using FunctionApp.Catalogs.Dtos;
using FunctionApp.Catalogs.Queries.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunctionApp.Catalogs.Handlers.Products
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly AppDbContext _appDbContext;
        public GetProductByIdHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _appDbContext.Products.SingleOrDefaultAsync(p => p.Id == request.Id);
            if (product == null) {
                throw new ArgumentException("Data is null");
            }

            return new ProductDto(product.Id, product.Name, product.Description, product.Price);
        }
    }
}