using System.ComponentModel.DataAnnotations;

namespace Project.DTOs.Put;

public class CompanyPutDto
{
    [MinLength(2)]
    public string? CompanyName { get; set; }
    [MinLength(5)]
    public string? Address { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
}