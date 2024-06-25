using System.ComponentModel.DataAnnotations;

namespace Project.DTOs.Post;

public class CompanyPostDto
{
    [Required] [MaxLength(100)] [MinLength(2)]
    public string CompanyName { get; set; } = string.Empty;

    [Required] [MaxLength(100)] [MinLength(5)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)] [EmailAddress]
    public string Email { get; set; }= string.Empty;

    [Required]
    [MaxLength(12)] [Phone]
    public string PhoneNumber { get; set; }= string.Empty;

    [Required]
    [MaxLength(14)]
    public string Krs { get; set; }= string.Empty;
}