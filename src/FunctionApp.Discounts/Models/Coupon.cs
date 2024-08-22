
public class Coupon
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Amount { get; set; }

    public Coupon(string productName, string description, int amount)
    {
        Id = Guid.NewGuid();
        ProductName = productName;
        Description = description;
        Amount = amount;
    }
    public Coupon()
    {
        
    }
}