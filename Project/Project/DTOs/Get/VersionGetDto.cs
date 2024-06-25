
namespace Project.DTOs.Get;

public class VersionGetDto
{
    public long IdVersion { get; set; }
    public string VersionNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Comments { get; set; } = string.Empty;
    public long IdSoftware { get; set; }
}