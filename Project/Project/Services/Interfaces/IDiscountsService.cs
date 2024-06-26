using Project.DTOs.Get;
using Project.DTOs.Post;

namespace Project.Services.Interfaces;

public interface IDiscountsService
{
    Task<DiscountGetDto> AddDiscount(DiscountPostDto dto);
}