using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.DTOs.Post;

public class SoftwarePostDto
{

    [Required] [MaxLength(100)] 
    public string Name { get; set; } = string.Empty;

    [Required] [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required] 
    public long IdCategory { get; set; }
    
    [Required]
    [DataType("decimal")]
    [Precision(18, 2)]
    public decimal Price { get; set; }
}