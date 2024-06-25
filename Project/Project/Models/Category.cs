using System.ComponentModel.DataAnnotations;

namespace Project.Models;

public class Category
{
    [Key] public long IdCategory { get; set; }

    [Required] [MaxLength(20)]
    public string CategoryName { get; set; } = string.Empty;


    public ICollection<Software> Softwares { get; set; } = new HashSet<Software>();
    
}