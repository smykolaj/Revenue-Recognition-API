using Project.DTOs.Post;
using Project.Models;

namespace Project.Repositories.Interfaces;

public interface IDiscountsRepository
{
    Task<Discount> AddDiscount(Discount newDiscount);
    Task<decimal> GetHighestDiscount();
}