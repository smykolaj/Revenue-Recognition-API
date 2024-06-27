using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.Exceptions;
using Project.Models;
using Project.Services.Interfaces;

namespace Project.Services;

public class ContractsService : IContractsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ContractsService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ContractGetDto> AddContract(ContractPostDto dto)
    {
        if (dto.TypeOfClient.Equals("Company") && !await _unitOfWork.Companies.ExistsById(dto.IdClient))
            throw new DoesntExistException("company", "idClient");

        if (dto.TypeOfClient.Equals("Individual") && !await _unitOfWork.Individuals.ExistsById(dto.IdClient))
            throw new DoesntExistException("individual", "idClient");
        
        if (! await _unitOfWork.Softwares.ExistsById(dto.IdSoftware))
            throw new DoesntExistException("software", "idSoftware");
        
        if (! await _unitOfWork.Versions.ExistsById(dto.IdVersion))
            throw new DoesntExistException("version", "idVersion");

        if (! await _unitOfWork.Versions.VersionIsAssociatedWith(dto.IdVersion, dto.IdSoftware))
            throw new Exception("The version is not associated with this software");
        
        if (!ContractDateRangeIsCorrect(dto.StartDate, dto.EndDate))
            throw new Exception("The time range of the contract has to be between 3 and 30 days.");

        if (dto.TypeOfClient.Equals("Individual") && await _unitOfWork.Contracts.IndividualHasActiveContract(dto.IdClient, dto.IdSoftware)
            || dto.TypeOfClient.Equals("Company") && await _unitOfWork.Contracts.CompanyHasActiveContract(dto.IdClient, dto.IdSoftware))
            throw new Exception("The client already has a contract associated with this software");

        var isReturningClient = await _unitOfWork.Contracts.HasAnyContracts(dto.TypeOfClient, dto.IdClient);
        var totalDiscountPercentage = await CalculateTotalDiscount(isReturningClient);
        var totalPrice = await CalculateTotalPrice(dto.IdSoftware, totalDiscountPercentage, dto.ContinuedSupportYears);
        
        var newContract = _mapper.Map(dto, totalPrice, _unitOfWork.Contracts.ContractStatusCreated, dto.TypeOfClient);
        var returnDto = _mapper.Map( await _unitOfWork.Contracts.AddContract(newContract));
        return returnDto;

    }

    public async Task<decimal> CalculateTotalPrice(long dtoIdSoftware, decimal totalDiscountPercentage, int continuedSupportYears)
    {
        return (await _unitOfWork.Softwares.GetSoftwarePrice(dtoIdSoftware) + 1000 * (continuedSupportYears - 1)) * (1 - totalDiscountPercentage / 100); 
    }

    public async Task<PaymentGetDto> AddPayment(long idContract, decimal amount)
    {
        if (!await _unitOfWork.Contracts.ExistsById(idContract))
            throw new DoesntExistException("contract", nameof(idContract));

        if (!await _unitOfWork.Contracts.ContractCanBePaid(idContract))
        {
            await _unitOfWork.Payments.SetAllContractPaymentsToCancelled(idContract);
            throw new Exception("Date for payment has finished. " +
                                "\n All previous payments will be cancelled." +
                                "\n Please create a new contract");
        }

        if (!await _unitOfWork.Contracts.NewPaymentDoesntExceedFullPrice(idContract, amount))
            throw new Exception("Payment is too big. Transaction is cancelled");
        var isFull = await _unitOfWork.Contracts.AmountEqualsFullPrice(idContract, amount);
        string status;
        
        if (isFull)
        {
            status = _unitOfWork.Payments.PaymentStatusFull;
            await _unitOfWork.Contracts.SetStatusToSigned(idContract);
        }
        else
        {
            status = _unitOfWork.Payments.PaymentStatusPartial;
            await _unitOfWork.Contracts.SetStatusToPending(idContract);
        }
        var newPayment = new Payment
        {
            Amount = amount,
            Status = status,
            Date = DateTime.Now,
            IdContract = idContract
        };
        newPayment=  await _unitOfWork.Payments.AddPayment(newPayment);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map(newPayment);
    }

    public async Task<decimal> CalculateTotalDiscount(bool isReturningClient)
    {
        decimal discount = 0;
        if (isReturningClient)
        {
            discount += 5;
        }

        discount += await _unitOfWork.Discounts.GetHighestDiscount();

        return discount;
    }

    public bool ContractDateRangeIsCorrect(DateTime dtoStartDate, DateTime dtoEndDate)
    {
        return (dtoEndDate.Subtract(dtoStartDate).Days <= 30) && (dtoEndDate.Subtract(dtoStartDate).Days >= 3);  
        
    }
}