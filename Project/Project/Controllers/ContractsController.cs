using Microsoft.AspNetCore.Mvc;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.Exceptions;
using Project.Services.Interfaces;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly IContractsService _contractsService;

    public ContractsController(IContractsService contractsService)
    {
        _contractsService = contractsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract(ContractPostDto dto)
    {
        try
        {
            ContractGetDto newContract = await _contractsService.AddContract(dto);
            return Ok(newContract);
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

    [HttpPost("{idContract}/pay")]
    public async Task<IActionResult> PayForContract(ContractPostDto dto)
    {
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok();
    }
}