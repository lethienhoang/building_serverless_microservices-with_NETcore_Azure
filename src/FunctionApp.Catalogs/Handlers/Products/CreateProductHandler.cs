using FunctionApp.Catalogs.Commands.Products;
using FunctionApp.Catalogs.Dtos;
using FunctionApp.Catalogs.Models;
using MediatR;

namespace FunctionApp.Catalogs.Handlers.Products;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductDto>
{
    private readonly AppDbContext _appDbContext;
    public CreateProductHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<CreateProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Id = Guid.NewGuid(),
            Category = request.Category,
            ImageFile = request.ImageFile,
            Price = request.Price,
        };

        await _appDbContext.AddAsync(product);
        await _appDbContext.SaveChangesAsync();

        return new CreateProductDto(product.Id);
    }
}