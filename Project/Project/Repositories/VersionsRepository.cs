using Project.Context;
using Project.Repositories.Interfaces;

namespace Project.Repositories;

public class VersionsRepository : IVersionsRepository
{
    private readonly ProjectContext _context;

    public VersionsRepository(ProjectContext context)
    {
        _context = context;
    }
}