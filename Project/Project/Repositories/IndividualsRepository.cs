using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories;

public class IndividualsRepository : IIndividualsRepository
{
    private readonly ProjectContext _context;

    public IndividualsRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByPesel(string clientPesel)
    {
        return await _context.Individuals.AnyAsync(i => i.Pesel.Equals(clientPesel) && !i.IsDeleted);
    }

    public async Task<bool> ExistsByEmail(string clientEmail)
    {
        return await _context.Individuals.AnyAsync(i => i.Email.Equals(clientEmail) && !i.IsDeleted);
    }

    public async Task<bool> ExistsByPhoneNumber(string clientPhoneNumber)
    {
        return await _context.Individuals.AnyAsync(i => i.PhoneNumber.Equals(clientPhoneNumber) && !i.IsDeleted);
    }

    public async Task<Individual> AddIndividual(Individual newIndividual)
    {
        await _context.AddAsync(newIndividual);
        await _context.SaveChangesAsync();
        return newIndividual;
    }

    public async Task<bool> ExistsById(long idIndividual)
    {
        return await _context.Individuals.AnyAsync(i => i.IdIndividual.Equals(idIndividual) && !i.IsDeleted);
    }

    public async Task<Individual> GetById(long idIndividual)
    {
        return await _context.Individuals.FirstAsync(i => i.IdIndividual.Equals(idIndividual));
    }

    public void DeleteIndividual(Individual individual)
    {
         _context.Remove(individual);
    }

    public async Task<Individual> UpdateIndividual(Individual newVersionOfIndividual, Individual oldIndividual)
    {
         _context.Entry(oldIndividual).CurrentValues.SetValues(newVersionOfIndividual);
         await _context.SaveChangesAsync();
         return oldIndividual;
    }
}