namespace Project.DTOs.Get;

public class DiscountGetDto
{
    public long IdDiscount { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Offer { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }

}