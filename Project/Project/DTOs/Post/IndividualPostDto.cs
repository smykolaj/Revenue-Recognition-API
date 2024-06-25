using System.ComponentModel.DataAnnotations;

namespace Project.DTOs.Post;

public class IndividualPostDto
{
    [Required] [MaxLength(100)] [MinLength(2)]
    public string FirstName { get; set; }= string.Empty;

    [Required] [MaxLength(100)] [MinLength(2)]
    public string LastName { get; set; }= string.Empty;

    [Required] [MaxLength(100)] [MinLength(5)]
    public string Address { get; set; }= string.Empty;

    [Required] [MaxLength(100)] [EmailAddress]
    public string Email { get; set; }= string.Empty;

    [Required] [MaxLength(12)] [Phone]
    public string PhoneNumber { get; set; }= string.Empty;

    [Required] [MaxLength(11)]
    public string Pesel { get; set; }= string.Empty;
}