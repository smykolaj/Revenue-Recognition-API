using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class Company
{
    [Key]
    public long IdCompany { get; set; }

    [Required] [MaxLength(100)] 
    public string CompanyName { get; set; } = string.Empty;

    [Required] [MaxLength(100)] 
    public string Address { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Email { get; set; }= string.Empty;

    [Required]
    [MaxLength(12)]
    public string PhoneNumber { get; set; }= string.Empty;

    [Required]
    [MaxLength(14)]
    public string Krs { get; set; }= string.Empty;

    public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
}