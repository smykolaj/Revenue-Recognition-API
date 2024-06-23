using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project.Models;

[PrimaryKey(nameof(IdPayment), nameof(IdContract))]
public class PaymentContract
{

    public int IdPayment { get; set; }
    [ForeignKey(nameof(IdPayment))] 
    public Payment Payment { get; set; } = null!;

    
    public long IdContract { get; set; }
    [ForeignKey(nameof(IdContract))] 
    public Contract Contract { get; set; } = null!;
}