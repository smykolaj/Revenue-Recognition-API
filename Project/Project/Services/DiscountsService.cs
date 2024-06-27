using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.Models;
using Project.Services.Interfaces;

namespace Project.Services;

public class DiscountsService : IDiscountsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DiscountsService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<DiscountGetDto> AddDiscount(DiscountPostDto dto)
    {
        if (dto.TimeStart > dto.TimeEnd)
        {
            throw new Exception("Time of start has to be before the end");
            

        }
        Discount newDiscount = _mapper.Map(dto);
        DiscountGetDto returnDto = _mapper.Map( await _unitOfWork.Discounts.AddDiscount(newDiscount));
        return returnDto;
    }
}