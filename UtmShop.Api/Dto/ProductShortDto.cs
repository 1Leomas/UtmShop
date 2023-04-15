namespace UtmShop.Api.Dto;

public class ProductShortDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
}