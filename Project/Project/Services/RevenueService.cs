using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using Project.DTOs.Get;
using Project.Exceptions;
using Project.Services.Interfaces;

namespace Project.Services
{
    public class RevenueService : IRevenueService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RevenueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RevenueGetDto> CalculateCompanyRevenue(string? currency, bool includePredictedRevenue)
        {
            decimal rate = 1;
            decimal revenue = 0;

            if (currency is not null && !currency.ToLower().Equals("pln"))
            {
                    rate = await GetExchangeRate(currency);
                
            }
            if (currency is null || currency.ToLower().Equals("pln"))
            {
                currency = "pln";
            }

            var contracts = await _unitOfWork.Contracts.GetAllAsync();
            if (!includePredictedRevenue)
                revenue = contracts.Where(c => c.Status.Equals("Signed"))
                    .Sum(c => c.FullPrice) / rate;
            else
            {
                revenue = contracts.Where(c => c.Status.Equals("Signed") || c.EndDate > DateTime.Now)
                    .Sum(c => c.FullPrice) / rate;
            }

            RevenueGetDto dto = new RevenueGetDto()
            {
                Type = includePredictedRevenue ? "Predicted" : "Current",
                Currency = currency,
                Date = DateTime.Today,
                Revenue = revenue
            };
            return dto;
        }

        public async Task<RevenueGetDto> CalculateProductRevenue(long idSoftware, string? currency, bool includePredictedRevenue)
        {
            decimal rate = 1;
            decimal revenue = 0;

            if (currency is not null && !currency.ToLower().Equals("pln"))
            {
                   rate = await GetExchangeRate(currency);
               
            }
            if (currency is null || currency.ToLower().Equals("pln"))
            {
                currency = "pln";
            }

            var contracts = await _unitOfWork.Contracts.GetAllAsync();
            contracts = contracts.Where(c => c.IdSoftware.Equals(idSoftware));
            var enumerable = contracts.ToList();
            if (enumerable.IsNullOrEmpty())
            {
                throw new DoesntExistException("contract", nameof(idSoftware));
            }
            if (!includePredictedRevenue)
                revenue = enumerable.Where(c => c.Status.Equals("Signed"))
                    .Sum(c => c.FullPrice) / rate;
            else
            {
                revenue = enumerable.Where(c => c.Status.Equals("Signed") || c.EndDate > DateTime.Now)
                    .Sum(c => c.FullPrice) / rate;
            }

            RevenueGetDto dto = new RevenueGetDto()
            {
                Type = includePredictedRevenue ? "Predicted" : "Current",
                Currency = currency,
                Date = DateTime.Today,
                Revenue = revenue
            };
            return dto;
        }

        public async Task<decimal> GetExchangeRate(string currency)
        {
            try
            {
                string url = $"http://api.nbp.pl/api/exchangerates/rates/a/{currency}/?format=json";

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);

                var responseBody = await response.Content.ReadAsStringAsync();
                JsonDocument document = JsonDocument.Parse(responseBody);
                JsonElement root = document.RootElement;

                JsonElement ratesArray = root.GetProperty("rates");
                JsonElement firstRateObject = ratesArray[0];
                decimal midValue = firstRateObject.GetProperty("mid").GetDecimal();
                return midValue;
            }
            catch (Exception)
            {
                throw new Exception($"Error generating exchange rate for {currency}");
            }
        }
        
    }
}
