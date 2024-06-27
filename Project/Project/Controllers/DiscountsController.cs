using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.Exceptions;
using Project.Services.Interfaces;

namespace Project.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DiscountsController : ControllerBase
{

    private readonly IDiscountsService _discountsService;

    public DiscountsController(IDiscountsService discountsService)
    {
        _discountsService = discountsService;
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddNewDiscount(DiscountPostDto dto)
    {
        try
        {
            DiscountGetDto newDiscount = await  _discountsService.AddDiscount(dto);
            return Ok(newDiscount);
        }catch (DoesntExistException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
}