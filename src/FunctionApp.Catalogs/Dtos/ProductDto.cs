namespace FunctionApp.Catalogs.Dtos
{
    public record ProductDto(Guid Id, string name, string description, decimal price);
}