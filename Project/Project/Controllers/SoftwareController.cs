using Microsoft.AspNetCore.Mvc;
using Project.DTOs.Get;
using Project.DTOs.Post;
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


    [HttpPost]
    public async Task<IActionResult> AddNewSoftware(SoftwarePostDto dto)
    {
        try
        {
            SoftwareGetDto newSoftware = await  _softwareService.AddSoftware(dto);
            return Ok(newSoftware);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
    
    [HttpPost("categories")]
    public async Task<IActionResult> AddNewCategory(CategoryPostDto dto)
    {
        try
        {
            CategoryGetDto newCategory = await  _softwareService.AddCategory(dto);
            return Ok(newCategory);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
    
    [HttpPost("{idSoftware:long}/versions")]
    public async Task<IActionResult> AddNewSoftwareVersion(long idSoftware, VersionPostDto dto)
    {
        try
        {
            VersionGetDto newVersion = await  _softwareService.AddSoftwareVersion(dto);
            return Ok(newVersion);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
}