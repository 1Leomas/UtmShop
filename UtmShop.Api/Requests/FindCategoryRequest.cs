using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UtmShop.Api.DAL;

namespace UtmShop.Api.Requests;

public class FindCategoryRequest : IRequest<long?>
{
    public string CategoryTitle { get; set; }

    public FindCategoryRequest(string categoryTitle)
    {
        CategoryTitle = categoryTitle;
    }
    internal class FindCategoryRequestHandler : IRequestHandler<FindCategoryRequest, long?>
    {
        private readonly ShopDbContext _context;

        public FindCategoryRequestHandler(ShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<long?> Handle(FindCategoryRequest request, CancellationToken cancellationToken)
        {
            var reqResult = await _context.Categories.FirstOrDefaultAsync(x => x.Title == request.CategoryTitle, cancellationToken);
            return reqResult?.Id;
        }
    }
}