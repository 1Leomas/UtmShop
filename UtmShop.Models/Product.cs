namespace UtmShop.Models;

public class Product
{
    public long Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public Category ParentCategory { get; set; }
}