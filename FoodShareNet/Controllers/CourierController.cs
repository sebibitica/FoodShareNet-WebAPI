using Microsoft.AspNetCore.Mvc;
using FoodShareNet.Domain.Entities;
using FoodShareNet.Application.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class CourierController : ControllerBase
{
    private readonly ICourierService _courierService;

    public CourierController(ICourierService courierService)
    {
        _courierService = courierService;
    }

    [ProducesResponseType(typeof(IList<CourierDTO>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<IList<CourierDTO>>> GetAllAsync()
    {
        var couriers = await _courierService.GetAllAsync();
        var couriersDTO = couriers
            .Select(c => new CourierDTO
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price
            });
        
        return Ok(couriersDTO);
    }

}
