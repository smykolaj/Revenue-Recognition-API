namespace Project.DTOs;

using System.ComponentModel.DataAnnotations;



public class User
{
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class VerifyPasswordRequestModel
{
    public string Password { get; set; } = null!;
    public string Hash { get; set; } = null!;
}

public class LoginRequestModel
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}

public class LoginResponseModel
{
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}

public class RefreshTokenRequestModel
{
    public string RefreshToken { get; set; } = null!;
}

public class RegisterRequest
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string Login { get; set; } = null!;

}