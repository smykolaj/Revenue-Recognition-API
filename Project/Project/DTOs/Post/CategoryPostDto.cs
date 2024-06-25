using System.ComponentModel.DataAnnotations;

namespace Project.DTOs.Post;

public class CategoryPostDto
{
    [Required] [MaxLength(20)]
    public string CategoryName { get; set; } = string.Empty;
}