// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using Microsoft.EntityFrameworkCore;
//
// namespace Project.Models;
//
// [PrimaryKey(nameof(IdContract), nameof(IdDiscount))]
// public class ContractDiscount
// {
//     
//     public long IdContract { get; set; }
//     [ForeignKey(nameof(IdContract))] 
//     public Contract Contract { get; set; } = null!;
//
//     
//     public long IdDiscount { get; set; }
//     [ForeignKey(nameof(IdDiscount))] 
//     public Discount Discount { get; set; } = null!;
// }