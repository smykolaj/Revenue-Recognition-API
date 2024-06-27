using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories
{
    public class ContractsRepository : IContractsRepository
    {
        private readonly ProjectContext _context;

        public ContractsRepository(ProjectContext context)
        {
            _context = context;
        }

        public string ContractStatusCreated => "Created";
        public string ContractStatusPendingFullPayment => "Pending";
        public string ContractStatusSigned => "Signed";

        public async Task<bool> IndividualHasActiveContract(long dtoIdClient, long dtoIdSoftware)
        {
            return await _context.Contracts.AnyAsync(c =>
                c.IdSoftware == dtoIdSoftware &&
                c.IdIndividual == dtoIdClient &&
                c.StartDate.AddYears(c.ContinuedSupportYears) > DateTime.Now);
        }

        public async Task<bool> CompanyHasActiveContract(long dtoIdClient, long dtoIdSoftware)
        {
            return await _context.Contracts.AnyAsync(c =>
                c.IdSoftware == dtoIdSoftware &&
                c.IdCompany == dtoIdClient &&
                c.StartDate.AddYears(c.ContinuedSupportYears) > DateTime.Now);
        }

        public async Task<bool> HasAnyContracts(string dtoTypeOfClient, long dtoIdClient)
        {
            if (dtoTypeOfClient.Equals("Company"))
            {
                return await _context.Contracts.AnyAsync(c => c.IdCompany == dtoIdClient);
            }

            return await _context.Contracts.AnyAsync(c => c.IdIndividual == dtoIdClient);
        }

        public async Task<Contract> AddContract(Contract newContract)
        {
            await _context.Contracts.AddAsync(newContract);
            await _context.SaveChangesAsync(); // Save changes
            return newContract;
        }

        public async Task<bool> ExistsById(long idContract)
        {
            return await _context.Contracts.AnyAsync(c => c.IdContract == idContract);
        }

        public async Task<bool> ContractCanBePaid(long idContract)
        {
            return await _context.Contracts.AnyAsync(c => c.IdContract == idContract &&
                                                          !c.Status.Equals(ContractStatusSigned) &&
                                                          c.EndDate > DateTime.Now);
        }

        public async Task<bool> NewPaymentDoesntExceedFullPrice(long idContract, decimal amount)
        {
            return await _context
                       .Contracts
                       .Where(c => c.IdContract == idContract)
                       .Select(c => c.FullPrice).FirstAsync()
                   >=
                   amount + await GetContractPaymentsSum(idContract);
        }

        public async Task<bool> AmountEqualsFullPrice(long idContract, decimal amount)
        {
            return await _context
                       .Contracts
                       .Where(c => c.IdContract == idContract)
                       .Select(c => c.FullPrice).FirstAsync()
                   ==
                   amount + await GetContractPaymentsSum(idContract);
        }

        public async Task SetStatusToSigned(long idContract)
        {
            var contract = await _context
                .Contracts
                .Where(c => c.IdContract == idContract).FirstAsync();
            contract.Status = ContractStatusSigned;
            await _context.SaveChangesAsync(); // Save changes
        }

        public async Task SetStatusToPending(long idContract)
        {
            var contract = await _context
                .Contracts
                .Where(c => c.IdContract == idContract).FirstAsync();
            contract.Status = ContractStatusPendingFullPayment;
            await _context.SaveChangesAsync(); // Save changes
        }

        public async Task<decimal> GetContractPaymentsSum(long idContract)
        {
            return await _context.Payments
                .Where(p => p.IdContract == idContract)
                .GroupBy(p => p.IdContract)
                .Select(g => g.Sum(p => p.Amount))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Contract>> GetAllAsync()
        {
            return await _context.Contracts.ToListAsync();
        }

        public async Task<IEnumerable<Contract>> GetBySoftwareIdAsync(long softwareId)
        {
            return await _context.Contracts.Where(c => c.IdSoftware == softwareId).ToListAsync();
        }

        public Task<Contract> GetById(long contractId)
        {
            throw new NotImplementedException();
        }
    }
}

    