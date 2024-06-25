using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Models;

public class Payment
{
    [Key]
    public int IdPayment { get; set; }

    [Required]
    [DataType("decimal")]
    [Precision(18, 2)]
    public decimal Amount { get; set; }

    [Required] [MaxLength(10)] 
    public string Status { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }


    public long IdContract { get; set; }
    [ForeignKey(nameof(IdContract))]
    public Contract Contract { get; set; } = null!;
}