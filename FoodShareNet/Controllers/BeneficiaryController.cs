using Microsoft.AspNetCore.Mvc;
using FoodShareNetAPI.DTO.Beneficiary;
using FoodShareNet.Domain.Entities;
using FoodShareNet.Application.Interfaces;

namespace FoodShareNetAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BeneficiaryController : ControllerBase
{
    private readonly IBeneficiaryService _beneficiaryService;

    public BeneficiaryController(IBeneficiaryService beneficiaryService)
    {
        _beneficiaryService=beneficiaryService;
    }

    [ProducesResponseType(typeof(IList<BeneficiaryDTO>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<IList<BeneficiaryDTO>>> GetAllAsync()
    {
        var beneficiaries = await _beneficiaryService.GetAllAsync();

        var beneficiaryDTOs = beneficiaries.Select(b => new BeneficiaryDTO
        {
            Id = b.Id,
            Name = b.Name,
            Address = b.Address,
            CityName = b.City.Name,
            Capacity = b.Capacity
        }).ToList();
        return Ok(beneficiaryDTOs);
    }

    [ProducesResponseType(typeof(BeneficiaryDTO),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<BeneficiaryDTO>> GetAsync(int id)
    {
        var beneficiary = await _beneficiaryService.GetAsync(id);

        var beneficiaryDTO = new BeneficiaryDTO 
        {
                Id = beneficiary.Id,
                Name = beneficiary.Name,
                Address = beneficiary.Address,
                CityName = beneficiary.City.Name,
                Capacity = beneficiary.Capacity
        };

        return Ok(beneficiaryDTO);
    }
    
    [ProducesResponseType(typeof(BeneficiaryDetailDTO), 
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<BeneficiaryDetailDTO>> 
        CreateAsync(CreateBeneficiaryDTO createBeneficiaryDTO)
    {

        var beneficiary = new Beneficiary
        {
            Name = createBeneficiaryDTO.Name,
            Address = createBeneficiaryDTO.Address,
            CityId = createBeneficiaryDTO.CityId,
            Capacity = createBeneficiaryDTO.Capacity
        };

        await _beneficiaryService.CreateAsync(beneficiary);

        var beneficiaryEntityDTO = new BeneficiaryDetailDTO
        {
            Id = beneficiary.Id,
            Name = beneficiary.Name,
            Address = beneficiary.Address,
            CityId = beneficiary.CityId,
            Capacity = beneficiary.Capacity
        };
        return Ok(beneficiaryEntityDTO);
    }

   
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<IActionResult> 
        EditAsync(int id, EditBeneficiaryDTO editBeneficiaryDTO)
    {
        if (id != editBeneficiaryDTO.Id)
        {
            return BadRequest("Mismatched Beneficiary ID");
        }

        var beneficiary = new Beneficiary
        {
            Id = editBeneficiaryDTO.Id,
            Name = editBeneficiaryDTO.Name,
            Address = editBeneficiaryDTO.Address,
            CityId = editBeneficiaryDTO.CityId,
            Capacity = editBeneficiaryDTO.Capacity
        };

        await _beneficiaryService.EditAsync(id,beneficiary);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _beneficiaryService.DeleteAsync(id);
        return NoContent();
    }
}
