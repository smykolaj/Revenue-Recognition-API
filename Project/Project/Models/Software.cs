using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Project.Models;


public class Software
{
    [Key]
    public long IdSoftware { get; set; }

    [Required] [MaxLength(100)] 
    public string Name { get; set; } = string.Empty;

    [Required] [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required] [MaxLength(100)]
    public string Category { get; set; } = string.Empty;
    
    [Required]
    [DataType("decimal")]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
    public ICollection<Version> Versions { get; set; } = new HashSet<Version>();
}