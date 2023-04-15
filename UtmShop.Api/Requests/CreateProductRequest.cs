using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UtmShop.Api.DAL;
using UtmShop.Api.Dto;
using UtmShop.Models;

namespace UtmShop.Api.Requests;

public class CreateProductRequest : IRequest<Product>
{
    public CreateProductDto CreateProductDto { get; set; }
    public long CategoryId { get; set; }

    public CreateProductRequest(CreateProductDto createProductDto, long categoryId)
    {
        CreateProductDto = createProductDto;
        CategoryId = categoryId;
    }

    internal class CreateProductRequestHandler: IRequestHandler<CreateProductRequest, Product>
    {
        protected readonly ShopDbContext _context;

        public CreateProductRequestHandler(ShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(src =>src.Id == request.CategoryId, cancellationToken);

            if (category == null) return null;

            var product = new Product
            {
                Title = request.CreateProductDto.Title,
                Price = request.CreateProductDto.Price
            };

            category.Products.Add(product);

            await _context.SaveChangesAsync(cancellationToken);

            return product;
        }
    }
}