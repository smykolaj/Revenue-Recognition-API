using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class AppUser
{
    [Key]
    public int IdUser { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExp { get; set; }
    public bool IsAdmin { get; set; } = false;
}