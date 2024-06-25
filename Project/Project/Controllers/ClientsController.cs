using Microsoft.AspNetCore.Mvc;
using Project.DTOs;
using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.DTOs.Put;
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
    [Tags("Create a new client")]
    [HttpPost("individuals")]
    public async Task<IActionResult> AddIndividualClient(IndividualPostDto client)
    {
        try
        {
           var newIndividual = await  _clientsService.AddIndividualClient(client);
           return Ok(newIndividual);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
        
        
    }
    
    [Tags("Create a new client")]
    [HttpPost("companies")]
    public async Task<IActionResult> AddCompanyClient(CompanyPostDto client)
    {
        try
        {
            CompanyGetDto newCompany = await  _clientsService.AddCompanyClient(client);
            return Ok(newCompany);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
        
        
    }
    
    [Tags("Delete a client")]
    [HttpDelete("individuals/{idIndividual:long}")]
    public async Task<IActionResult> RemoveClient(long idIndividual)
    {
        try
        {
            await _clientsService.SoftDeleteIndividualClient(idIndividual);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }

    [HttpPut("individuals/{idIndividual:long}")]
    public async Task<IActionResult> UpdateDataAboutIndividual(long idIndividual, IndividualPutDto client)
    {
        try
        {
            await _clientsService.UpdateDataAboutIndividual(idIndividual, client);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }
    
    [HttpPut("companies/{idCompany:long}")]
    public async Task<IActionResult> UpdateDataAboutCompany(long idCompany, CompanyPutDto client)
    {
        try
        {
            await _clientsService.UpdateDataAboutCompany(idCompany, client);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        } 
    }

}