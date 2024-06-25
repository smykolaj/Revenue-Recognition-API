namespace Project.DTOs.Get;

public class SoftwareGetDto
{
    public long IdSoftware { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long IdCategory { get; set; }
    public decimal Price { get; set; }
}