namespace FunctionApp.Catalogs.Models;

public class Product {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<string> Category { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }
    public decimal Price { get; set; }

}