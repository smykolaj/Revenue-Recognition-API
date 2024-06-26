namespace Project.DTOs.Get;

public class ContractGetDto
{
    public long IdContract { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long IdClient { get; set; }
    public string TypeOfClient { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal FullPrice { get; set; }
    public long IdSoftware { get; set; }
    public long IdVersion { get; set; }
    public int ContinuedSupportYears { get; set; }

}