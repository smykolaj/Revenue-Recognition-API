using Project.DTOs.Post;
using Project.Models;

namespace Project.Repositories.Interfaces;

public interface IContractsRepository
{
    Task<bool> IndividualHasActiveContract(long dtoIdClient, long dtoIdSoftware);
    Task<bool> CompanyHasActiveContract(long dtoIdClient, long dtoIdSoftware);
    Task<bool> HasAnyContracts(string dtoTypeOfClient, long dtoIdClient);
    Task<Contract> AddContract(Contract newContract);
}