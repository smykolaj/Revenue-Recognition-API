using Project.DTOs.Post;
using Project.Models;

namespace Project.Repositories.Interfaces;

public interface IContractsRepository
{
    string ContractStatusCreated { get; }
    string ContractStatusPendingFullPayment { get; }
    string ContractStatusSigned { get; }
    Task<bool> IndividualHasActiveContract(long dtoIdClient, long dtoIdSoftware);
    Task<bool> CompanyHasActiveContract(long dtoIdClient, long dtoIdSoftware);
    Task<bool> HasAnyContracts(string dtoTypeOfClient, long dtoIdClient);
    Task<Contract> AddContract(Contract newContract);
    Task<bool> ExistsById(long idContract);
    Task<bool> ContractCanBePaid(long idContract);
    Task<bool> NewPaymentDoesntExceedFullPrice(long idContract, decimal amount);
    Task<bool> AmountEqualsFullPrice(long idContract, decimal amount);
    Task SetStatusToSigned(long idContract);
    Task SetStatusToPending(long idContract);
    Task<decimal> GetContractPaymentsSum(long idcontract);
    Task<IEnumerable<Contract>> GetAllAsync();
    Task<IEnumerable<Contract>> GetBySoftwareIdAsync(long softwareId); 
    Task<Contract> GetById(long contractId);
}