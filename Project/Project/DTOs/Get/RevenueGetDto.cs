namespace Project.DTOs.Get;

public class RevenueGetDto
{
    public string Type { get; set; }
    public decimal Revenue { get; set; }
    public string Currency { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
}