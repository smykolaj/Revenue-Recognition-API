using Project.Context;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories;

public class SoftwaresRepository : ISoftwaresRepository
{
    private readonly ProjectContext _context;

    public SoftwaresRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<Software> AddSoftware(Software newSoftware)
    {
        await _context.AddAsync(newSoftware);
        await _context.SaveChangesAsync();
        return newSoftware;
    }
}