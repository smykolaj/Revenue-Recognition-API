using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.DTOs.Post;

public class VersionPostDto
{
    [Required] [MaxLength(100)]
    public string VersionNumber { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }
    
    [Required] [MaxLength(100)]
    public string Comments { get; set; } = string.Empty;

   
}