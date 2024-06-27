using Project.DTOs.Get;
using Project.DTOs.Post;

namespace Project.Services.Interfaces;

public interface IContractsService
{
    Task<ContractGetDto> AddContract(ContractPostDto dto);
    bool ContractDateRangeIsCorrect(DateTime dtoStartDate, DateTime dtoEndDate);
    Task<decimal> CalculateTotalDiscount(bool isReturningClient);
    Task<decimal> CalculateTotalPrice(long dtoIdSoftware, decimal totalDiscountPercentage, int continuedSupportYears);
    Task<PaymentGetDto> AddPayment(long idContract, decimal amount);
}