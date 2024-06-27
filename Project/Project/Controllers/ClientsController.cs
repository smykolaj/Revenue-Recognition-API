using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTOs;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.DTOs.Put;
using Project.Exceptions;
using Project.Services.Interfaces;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ClientsController : ControllerBase
{

    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }
    [Authorize]
    [HttpPost("individuals")]
    public async Task<IActionResult> AddIndividualClient(IndividualPostDto client)
    {
        try
        {
            var newIndividual = await _clientsService.AddIndividualClient(client);
            return Ok(newIndividual);
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
    [HttpPost("companies")]
    public async Task<IActionResult> AddCompanyClient(CompanyPostDto client)
    {
        try
        {
            CompanyGetDto newCompany = await  _clientsService.AddCompanyClient(client);
            return Ok(newCompany);
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
    [HttpDelete("individuals/{idIndividual:long}")]
    public async Task<IActionResult> RemoveClient(long idIndividual)
    {
        if (!User.IsInRole("admin"))
        {
            return Unauthorized();
        }
        
        try
        {
            await _clientsService.SoftDeleteIndividualClient(idIndividual);
            return Ok();
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
    [HttpPut("individuals/{idIndividual:long}")]
    public async Task<IActionResult> UpdateDataAboutIndividual(long idIndividual, IndividualPutDto client)
    {if (!User.IsInRole("admin"))
        {
            return Unauthorized();
        }

        try
        {
           var res =  await _clientsService.UpdateDataAboutIndividual(idIndividual, client);
            return Ok(res);
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
    [HttpPut("companies/{idCompany:long}")]
    public async Task<IActionResult> UpdateDataAboutCompany(long idCompany, CompanyPutDto client)
    {if (!User.IsInRole("admin"))
        {
            return Unauthorized();
        }

        try
        {
            var res = await _clientsService.UpdateDataAboutCompany(idCompany, client);
            return Ok(res);
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