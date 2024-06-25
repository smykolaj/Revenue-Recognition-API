using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories;

public class CompaniesRepository : ICompaniesRepository
{
    private readonly ProjectContext _context;

    public CompaniesRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByKrs(string clientKrs)
    {
        return await _context.Companies.AnyAsync(c => c.Krs.Equals(clientKrs));
    }

    public async Task<bool> ExistsByEmail(string clientEmail)
    {
        return await _context.Companies.AnyAsync(c => c.Email.Equals(clientEmail));
    }

    public async Task<bool> ExistsByPhoneNumber(string clientPhoneNumber)
    {
        return await _context.Companies.AnyAsync(c => c.PhoneNumber.Equals(clientPhoneNumber));
    }

    public async Task<Company> AddCompany(Company newCompany)
    {
        await _context.AddAsync(newCompany);
        await _context.SaveChangesAsync();
        return newCompany;
    }

    public async Task<bool> ExistsById(long idCompany)
    {
        return await _context.Companies.AnyAsync(c => c.IdCompany.Equals(idCompany));
    }

    public async Task<Company> GetById(long idCompany)
    {
        return await _context.Companies.FirstAsync(c => c.IdCompany.Equals(idCompany));
    }

    public async Task<Company> UpdateCompany(Company newCompany, Company oldCompany)
    {
        _context.Entry(oldCompany).CurrentValues.SetValues(newCompany);
        await _context.SaveChangesAsync();
        return oldCompany;
    }
}