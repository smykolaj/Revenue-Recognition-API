using Project.DTOs;
using Project.Models;

namespace Project.Repositories.Interfaces;

public interface ICompaniesRepository
{
    Task<bool> ExistsByKrs(string clientKrs);
    Task<bool> ExistsByEmail(string clientEmail);
    Task<bool> ExistsByPhoneNumber(string clientPhoneNumber);
    Task<Company> AddCompany(Company newCompany);
    Task<bool> ExistsById(long idCompany);
    Task<Company> GetById(long idCompany);
    Task<Company> UpdateCompany(Company newCompany, Company oldCompany);
}