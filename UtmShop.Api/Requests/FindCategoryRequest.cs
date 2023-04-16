using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UtmShop.Api.DAL;

namespace UtmShop.Api.Requests;

public class FindCategoryRequest : IRequest<List<long>?>
{
    public string CategoryTitle { get; set; }

    public FindCategoryRequest(string categoryTitle)
    {
        CategoryTitle = categoryTitle;
    }
    internal class FindCategoryRequestHandler : IRequestHandler<FindCategoryRequest, List<long>?>
    {
        private readonly ShopDbContext _context;

        public FindCategoryRequestHandler(ShopDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<long>?> Handle(FindCategoryRequest request, CancellationToken cancellationToken)
        { 
            var reqResult = await _context.Categories.Where(x => x.Title.Contains(request.CategoryTitle)).Select(x => x.Id)
                .ToListAsync(cancellationToken);

            return reqResult;
        }
    }
}