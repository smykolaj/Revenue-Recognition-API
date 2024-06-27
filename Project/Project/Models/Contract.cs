using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Models;

public class Contract
{
    [Key]
    public long IdContract { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required] [MaxLength(20)] [ConcurrencyCheck]   
    public string Status { get; set; } = string.Empty;

    [Required]
    [DataType("decimal")]
    [Precision(18, 2)]
    public decimal FullPrice { get; set; }

    public long? IdIndividual { get; set; }
    [ForeignKey(nameof(IdIndividual))]
    public Individual? Individual { get; set; }

    public long? IdCompany { get; set; }
    [ForeignKey(nameof(IdCompany))]
    public Company? Company { get; set; }

    [Required]
    public long IdSoftware { get; set; }
    [ForeignKey(nameof(IdSoftware))] 
    public Software Software { get; set; } = null!;
    
    [Required]
    public long IdVersion { get; set; }
    [ForeignKey(nameof(IdVersion))]
    public Version Version { get; set; }  = null!;
    
 

    [Required]
    public int ContinuedSupportYears { get; set; }

    
    // public ICollection<ContractDiscount> ContractDiscounts { get; set; } = new HashSet<ContractDiscount>();
    public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
}