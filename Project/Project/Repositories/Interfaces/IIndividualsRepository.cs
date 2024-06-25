using Project.Models;

namespace Project.Repositories.Interfaces;

public interface IIndividualsRepository
{
    Task<bool> ExistsByPesel(string clientPesel);
    Task<bool> ExistsByEmail(string clientEmail);
    Task<bool> ExistsByPhoneNumber(string clientPhoneNumber);
    Task<Individual> AddIndividual(Individual individual);
    Task<bool> ExistsById(long idIndividual);
    Task<Individual> GetById(long idIndividual);
    void DeleteIndividual(Individual individual);
    Task<Individual> UpdateIndividual(Individual newVersionOfIndividual, Individual oldIndividual);
}