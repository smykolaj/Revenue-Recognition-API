using Project.DTOs.Get;

namespace Project.Services.Interfaces;

public interface IRevenueService
{
    Task<RevenueGetDto> CalculateCompanyRevenue(string? currency, bool includePredictedRevenue);
    Task<RevenueGetDto> CalculateProductRevenue(long idSoftware, string? currency, bool includePredictedRevenue);
    Task<decimal> GetExchangeRate(string currency);
}