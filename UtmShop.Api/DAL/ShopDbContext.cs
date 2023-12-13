using Microsoft.EntityFrameworkCore;
using UtmShop.Models;

namespace UtmShop.Api.DAL;

public class ShopDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {

    }
}