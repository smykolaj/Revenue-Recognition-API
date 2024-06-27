using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.DTOs;
using Project.Services.Interfaces;

namespace JWT.Controllers;

[Route("api/[controller]")]
public class LoginController(ILoginService service) : ControllerBase
{

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequestModel model)
    {
        try
        { 
            LoginResponseModel answer = service.Login(model);
            return Ok(answer);
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }

       
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterStudent(RegisterRequest model)
    {
        try
        {
            service.RegisterUser(model);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
        
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public IActionResult Refresh(string refreshToken)
    {
        LoginResponseModel responseModel;
        try
        {
            responseModel = service.Refresh(refreshToken);
            return Ok(responseModel);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}