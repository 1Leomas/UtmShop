using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UtmShop.Api.Dto;
using UtmShop.Api.Requests;
using UtmShop.Models;

namespace UtmShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public CategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper;
    }

    // GET: api/<ShopManager>/categories
    [HttpGet("categories")]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetCategoriesRequest());

        if (result == null) 
            return NotFound();
        var finalResult = _mapper.Map<IList<Category>, IList<CategoryShortDto>>(result);
        return Ok(finalResult);
    }

    // GET api/<ShopManager>/categories/5
    [HttpGet("categories/{id:long}")]
    public async Task<IActionResult> GetSpecificCategory(long id)
    {
        var result = await _mediator.Send(new GetCategoriesRequest(id));

        if (result == null)
            return NotFound();
        var finalResult = _mapper.Map<Category, CategoryShortDto>(result.First());
        return Ok(finalResult);
    }

    [HttpGet("categories/{id:long}/products")]
    public async Task<IActionResult> GetCategoryProducts(long id)
    {
        var result = await _mediator.Send(new GetCategoryProductsRequest(id));
        if (result == null)
            return NotFound();
        return Ok(_mapper.Map<IList<Product>, IList<ProductShortDto>>(result));
    }

    [HttpPost("categories/{id:long}/products")]
    public async Task<IActionResult> GetCategoryProducts(long id, [FromBody] CreateProductDto product)
    {
        var result = await _mediator.Send((new CreateProductRequest(product, id)));
        if (result == null) return BadRequest();
        return Ok(_mapper.Map<Product, ProductShortDto>(result));
    }

    [HttpDelete("categories/products/{id:long}")]
    public async Task<IActionResult> DeleteProduct(long id)
    {
        var result = await _mediator.Send(new DeleteProductRequest(id));
        if (!result)
            return NotFound();
        return Ok();
    }

    [HttpGet("categories/search")]
    public async Task<IActionResult> FindCategory([FromQuery] string categoryName)
    {
        var result = await _mediator.Send(new FindCategoryRequest(categoryName));
        if (!result.HasValue)
            return NotFound();
        return Ok(result);
    }

    // POST api/<ShopManager>/categories
    [HttpPost("categories")]
    public async Task<IActionResult> Post([FromBody] CreateCategoryDto value)
    {
        var searchResult = await _mediator.Send(new FindCategoryRequest(value.Title));
        if (searchResult.HasValue)
            return Conflict();

        var createdCategory = await _mediator.Send(new CreateCategoryRequest(value.Title));
        if (!createdCategory.status)
            return BadRequest();
        return Ok(_mapper.Map<CategoryShortDto>(createdCategory.cat));
    }

    // PUT api/<ShopManager>/5
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Put(long id, [FromBody] CreateCategoryDto value)
    {
        var searchResult = await _mediator.Send(new FindCategoryRequest(value.Title));
        if (searchResult != null)
            return Conflict();
        var patchResult = await _mediator.Send(new ChangeCategoryTitleRequest(id, value.Title));
        if (patchResult == null)
            return BadRequest();
            
        return Ok(_mapper.Map<CategoryShortDto>(patchResult));
    }

    // DELETE api/<ShopManager>/categories/5
    [HttpDelete("categories/{id:long}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCategoryRequest(id));
        if (!result)
            return NotFound();
        return Ok();
    }
}