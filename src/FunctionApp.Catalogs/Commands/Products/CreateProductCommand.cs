using FunctionApp.Catalogs.Dtos;
using MediatR;

namespace FunctionApp.Catalogs.Commands.Products;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : IRequest<CreateProductDto>;
