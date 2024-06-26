using Project.DTOs.Post;
using Project.Models;

namespace Project.Repositories.Interfaces;

public interface ISoftwaresRepository
{
    Task<Software> AddSoftware(Software newSoftware);
    Task<bool> ExistsById(long dtoIdSoftware);
    Task<decimal> GetSoftwarePrice(long dtoIdSoftware);
}