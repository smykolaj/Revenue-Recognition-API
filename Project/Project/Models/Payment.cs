using System.ComponentModel.DataAnnotations;
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

    public ICollection<PaymentContract> PaymentContracts { get; set; } = new HashSet<PaymentContract>();
}