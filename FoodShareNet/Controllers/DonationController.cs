using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodShareNet.Domain.Entities;
using FoodShareNet.Repository.Data;
using FoodShareNetAPI.DTO.Donation;
using FoodShareNetAPI.DTO.Beneficiary;
using FoodShareNet.Application.Interfaces;

namespace FoodShareNetAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DonationController : ControllerBase
{
    private readonly IDonationService _donationService;

    public DonationController(IDonationService donationService)
    {
        _donationService = donationService;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> CreateDonation([FromBody] CreateDonationDTO donationDTO)
    {
        var donation = new Donation
        {
            DonorId = donationDTO.DonorId,
            ProductId = donationDTO.ProductId,
            Quantity = donationDTO.Quantity,
            ExpirationDate = donationDTO.ExpirationDate,
            StatusId = donationDTO.StatusId
        };

        await _donationService.CreateDonation(donation);

       var donationEntityDTO = new DonationDetailDTO
        {
            Id = donation.Id,
            DonorId = donation.DonorId,
            Product = donation.ProductId.ToString(),
            Quantity = donation.Quantity,
            ExpirationDate = donation.ExpirationDate,
            StatusId = donation.StatusId
        };

        return Ok(donationEntityDTO);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<DonationDetailDTO>> GetDonation(int id)
    {
        var donation = await _donationService.GetDonation(id);

        var donationDetailDTO = new DonationDetailDTO
        {
            Id = donation.Id,
            DonorId = donation.DonorId,
            Product = donation.Product.Name,
            Quantity = donation.Quantity,
            ExpirationDate = donation.ExpirationDate,
            StatusId = donation.StatusId,
            Status = donation.Status.Name
        };

        if (donationDetailDTO == null)
        {
            return NotFound();
        }
        return Ok(donationDetailDTO);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet()]
    public async Task<ActionResult<IList<DonationDetailDTO>>> GetDonationsByCityId(int cityId)
    {
        var donations = await _donationService.GetDonationsByCityId(cityId);

        var donationsDTO = donations
            .Select(d => new DonationDetailDTO
            {
                Id = d.Id,
                DonorId = d.DonorId,
                Product = d.Product.Name,
                Quantity = d.Quantity,
                ExpirationDate = d.ExpirationDate,
                StatusId = d.StatusId,
                Status = d.Status.Name
            });

        return Ok(donationsDTO);
    }

}
