using FoodShareNet.Domain.Entities;
using FoodShareNet.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodShareNetAPI.DTO.Donor;
using FoodShareNetAPI.DTO.Donation;
using FoodShareNet.Application.Interfaces; // Ensure you have the corresponding DTO namespace

namespace FoodShareNetAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DonorController : ControllerBase
{
    private readonly IDonorService _donorService;
    public DonorController(IDonorService donorService)
    {
        _donorService = donorService;
    }

    [ProducesResponseType(type: typeof(List<DonorDTO>) ,StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<IList<DonorDTO>>> GetAllAsync()
    {
        var donors = await _donorService.GetAllAsync();

        var donorDTOs = donors
            .Select(d => new DonorDTO
            {
                Id = d.Id,
                Name = d.Name,
                CityName = d.City.Name,
                Address = d.Address,
                Donations = d.Donations
                .Select(donation => new DonationDTO
                {
                    Id = donation.Id,
                    Product = donation.Product.Name,
                    Quantity = donation.Quantity,
                    ExpirationDate = donation.ExpirationDate,
                    Status = donation.Status.Name
                }).ToList()
            }).ToList();

        return Ok(donorDTOs);
    }
    [ProducesResponseType(type: typeof(List<DonorDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet()]
    public async Task<ActionResult<DonorDTO>> GetAsync(int id) 
    {
        var d = await _donorService.GetAsync(id);
        var donorDTO = new DonorDTO
        {
            Id = d.Id,
            Name = d.Name,
            CityName = d.City.Name,
            Address = d.Address,
            Donations = d.Donations
                .Select(donation => new DonationDTO
                {
                    Id = donation.Id,
                    Product = donation.Product.Name,
                    Quantity = donation.Quantity,
                    ExpirationDate = donation.ExpirationDate,
                    Status = donation.Status.Name
                }).ToList()
        };

        if (donorDTO == null)
        {
            return NotFound();
        }
        return Ok(donorDTO);
    }

    [ProducesResponseType(typeof(DonorDetailDTO),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<DonorDetailDTO>> CreateAsync(CreateDonorDTO createDonorDTO)
    {

        var donor = new Donor
        {
            Name= createDonorDTO.Name,
            CityId= createDonorDTO.CityId,
            Address = createDonorDTO.Address
        };

        await _donorService.CreateAsync(donor);

        var donorEntityDTO = new DonorDetailDTO
        {
            Id=donor.Id,
            Name=donor.Name,
            CityId = donor.CityId,
            Address = donor.Address
        };

        return Ok(donorEntityDTO);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut] 
    public async Task<IActionResult> EditAsync(int id, EditDonorDTO editDonorDTO)
    {
        if (id != editDonorDTO.Id)
        {
            return BadRequest("Mismatched Donor ID");
        }

        var donor = new Donor
        {
            Id = id,
            Name = editDonorDTO.Name,
            Address = editDonorDTO.Address,
            CityId = editDonorDTO.CityId,
        };

        await _donorService.EditAsync(id, donor);

        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete()]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _donorService.DeleteAsync(id);
        return NoContent();
    }
}
