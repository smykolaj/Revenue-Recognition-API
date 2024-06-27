using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Project.DTOs.Get;
using Project.Exceptions;
using Project.Services.Interfaces;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenueController : ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }

    [Authorize]
    [HttpGet("company")]
    public async Task<IActionResult> CalculateCompanyRevenue(string? currency, bool includePredictedRevenue)
    {
        try
        {
            RevenueGetDto result = await _revenueService.CalculateCompanyRevenue(currency, includePredictedRevenue);
            return Ok(result);
        }
        catch (DoesntExistException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    [Authorize]
    [HttpGet("software/{softwareId:long}")]
    public async Task<IActionResult> CalculateProductRevenue(long softwareId, string? currency, bool includePredictedRevenue)
    {
        try
        {
            RevenueGetDto result = await _revenueService.CalculateProductRevenue(softwareId, currency, includePredictedRevenue);
            return Ok(result);
        }
        catch (DoesntExistException e)
        {
            return NotFound(e.Message);
        }
        
        
    }

}