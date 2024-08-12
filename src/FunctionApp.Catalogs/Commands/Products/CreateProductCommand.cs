using System.Text.Json.Serialization;
using FunctionApp.Catalogs.Dtos;
using MediatR;

namespace FunctionApp.Catalogs.Commands.Products;

public record CreateProductCommand(
    [property: JsonPropertyName("name")] string Name,
   [property: JsonPropertyName("category")] List<string> Category,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("imageFile")] string ImageFile,
    [property: JsonPropertyName("price")] decimal Price) : IRequest<CreateProductDto>;
