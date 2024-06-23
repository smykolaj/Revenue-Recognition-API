using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class Individual
{
    [Key]
    public long IdIndividual { get; set; }

    [Required] [MaxLength(100)]
    public string FirstName { get; set; }= string.Empty;

    [Required] [MaxLength(100)]
    public string LastName { get; set; }= string.Empty;

    [Required] [MaxLength(100)]
    public string Address { get; set; }= string.Empty;

    [Required] [MaxLength(100)]
    public string Email { get; set; }= string.Empty;

    [Required] [MaxLength(12)]
    public string PhoneNumber { get; set; }= string.Empty;

    [Required] [MaxLength(11)]
    public string Pesel { get; set; }= string.Empty;

    public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
}