using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UtmShop.Api.DAL;

namespace UtmShop.Api.Requests;

public class DeleteProductRequest : IRequest<bool>
{
    public long ProductId { get; set; }

    public DeleteProductRequest(long productId)
    {
        ProductId = productId;
    }

    internal class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, bool>
    {
        private readonly ShopDbContext _context;
        private readonly ILogger<DeleteProductRequestHandler> _logger;

        public DeleteProductRequestHandler(ShopDbContext context, ILogger<DeleteProductRequestHandler> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var productToDelete = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);
            if (productToDelete == null)
                return false;
            _context.Products.Remove(productToDelete);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting data: {message}", ex.Message);
            }
            return false;
        }
    }
}