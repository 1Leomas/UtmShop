using UtmShop.Models;

public class Category
{
    public long Id { get; set; }
    public string Title { get; set; }
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
}