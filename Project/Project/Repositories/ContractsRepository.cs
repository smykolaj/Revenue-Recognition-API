using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Models;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

namespace Project.Repositories;

public class ContractsRepository : IContractsRepository
{
    private readonly ProjectContext _context;

    public ContractsRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<bool> IndividualHasActiveContract(long dtoIdClient, long dtoIdSoftware)
    {
        return await _context.Contracts.AnyAsync(c =>
            c.IdSoftware.Equals(dtoIdSoftware) &&
            c.IdIndividual.Equals(dtoIdClient) && 
            c.StartDate.AddYears( c.ContinuedSupportYears) > DateTime.Now );
    }

    public async Task<bool> CompanyHasActiveContract(long dtoIdClient, long dtoIdSoftware)
    {
        return await _context.Contracts.AnyAsync(c =>
            c.IdSoftware.Equals(dtoIdSoftware) &&
            c.IdCompany.Equals(dtoIdClient) && 
            c.StartDate.AddYears( c.ContinuedSupportYears) < DateTime.Now );
    }

    public async Task<bool> HasAnyContracts(string dtoTypeOfClient, long dtoIdClient)
    {
        if (dtoTypeOfClient.Equals("Company"))
        {
            return await _context.Contracts.AnyAsync(c => c.IdCompany.Equals(dtoIdClient)); 
        }

        return await _context.Contracts.AnyAsync(c => c.IdIndividual.Equals(dtoIdClient));
    }

    public async Task<Contract> AddContract(Contract newContract)
    {
        await _context.Contracts.AddAsync(newContract);
        await _context.SaveChangesAsync();
        return newContract;
    }
}