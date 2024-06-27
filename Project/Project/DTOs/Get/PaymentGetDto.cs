namespace Project.DTOs.Get;

public class PaymentGetDto
{
    public int IdPayment { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public long IdContract { get; set; }
}