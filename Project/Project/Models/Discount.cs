using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Project.Models;

public class Discount
{
    [Key]
    public long IdDiscount { get; set; }

    [Required] [MaxLength(100)] 
    public string Name { get; set; } = string.Empty;

    [Required] [MaxLength(100)]
    public string Offer { get; set; } = string.Empty;

    [Required]
    [DataType("decimal")]
    [Precision(3, 2)]
    public decimal Value { get; set; }

    [Required]
    public DateTime TimeStart { get; set; }

    [Required]
    public DateTime TimeEnd { get; set; }

    public ICollection<ContractDiscount> ContractDiscounts { get; set; } = new HashSet<ContractDiscount>();
}