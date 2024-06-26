using System.ComponentModel.DataAnnotations;

namespace Project.DTOs.Post;

public class ContractPostDto
{
    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public long IdClient { get; set; }

    [Required]
    [RegularExpression("Individual|Company", ErrorMessage = "TypeOfClient must be either 'Individual' or 'Company'.")]
    public string TypeOfClient { get; set; } = string.Empty;
    [Required]
    public long IdSoftware { get; set; } 
    [Required]
    public long IdVersion { get; set; }
    [Required] [RegularExpression("1|2|3|4", ErrorMessage = "Continued support can be from 1 to 4 years")]
    public int ContinuedSupportYears { get; set; }

}