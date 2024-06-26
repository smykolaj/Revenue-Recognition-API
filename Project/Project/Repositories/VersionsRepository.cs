using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.DTOs.Post;
using Project.Repositories.Interfaces;
using Version = Project.Models.Version;

namespace Project.Repositories;

public class VersionsRepository : IVersionsRepository
{
    private readonly ProjectContext _context;

    public VersionsRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<Version> AddVersion(Version newVersion)
    {
        await _context.AddAsync(newVersion);
        await _context.SaveChangesAsync();
        return newVersion;
    }

    public async Task<bool> ExistsById(long dtoIdVersion)
    {
        return await _context.Versions.AnyAsync(v => v.IdVersion.Equals(dtoIdVersion));

    }

    public async Task<bool> VersionIsAssociatedWith(long dtoIdVersion, long dtoIdSoftware)
    {
        return await _context.Versions.AnyAsync(
            v => v.IdVersion.Equals(dtoIdVersion) && v.IdSoftware.Equals(dtoIdSoftware));
    }
}