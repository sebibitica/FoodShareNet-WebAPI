using Microsoft.AspNetCore.Mvc;
using FoodShareNetAPI.DTO.Product;
using FoodShareNet.Repository.Data;
using FoodShareNetAPI.DTO.Beneficiary;
using Microsoft.EntityFrameworkCore;
using FoodShareNet.Application.Interfaces;
using FoodShareNet.Application.Services;


[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [ProducesResponseType(typeof(IList<ProductDTO>),
       StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<IList<ProductDTO>>> GetAllAsync()
    {
        var products = await _productService.GetAllAsync();
        var productDTOs = products
            .Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Image = p.Image
            });
        return Ok(productDTOs);
    }

}
