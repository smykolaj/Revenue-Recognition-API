using System.ComponentModel.DataAnnotations;

namespace Project.DTOs.Put;

public class IndividualPutDto
{
    [MinLength(2)]
    public string? FirstName { get; set; }
    [MinLength(2)]
    public string? LastName { get; set; }
    [MinLength(5)]
    public string? Address { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }

}