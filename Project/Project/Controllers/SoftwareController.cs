using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.Exceptions;
using Project.Services.Interfaces;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SoftwareController : ControllerBase
{
    private readonly ISoftwareService _softwareService;

    public SoftwareController(ISoftwareService softwareService)
    {
        _softwareService = softwareService;
    }
    
   
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddNewSoftware(SoftwarePostDto dto)
    {
        try
        {
            SoftwareGetDto newSoftware = await  _softwareService.AddSoftware(dto);
            return Ok(newSoftware);
        }catch (DoesntExistException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
    
    [Authorize]
    [HttpPost("categories")]
    public async Task<IActionResult> AddNewCategory(CategoryPostDto dto)
    {
        try
        {
            CategoryGetDto newCategory = await  _softwareService.AddCategory(dto);
            return Ok(newCategory);
        }catch (DoesntExistException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
    [Authorize]
    [HttpPost("{idSoftware:long}/versions")]
    public async Task<IActionResult> AddNewSoftwareVersion(long idSoftware, VersionPostDto dto)
    {
        try
        {
            VersionGetDto newVersion = await  _softwareService.AddSoftwareVersion(dto, idSoftware);
            return Ok(newVersion);
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