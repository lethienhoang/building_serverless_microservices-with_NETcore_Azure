public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Coupon> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasKey(p => p.Id);
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon("iPhone 15 Pro", "sale of 8% in valentine", 8),
            new Coupon("Dell XPS 15", "sale of 12%", 12),
            new Coupon("Sony WH-1000XM4", "sale of 30%",  30)
        );
    }
}