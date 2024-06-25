using Project.Context;
using Project.Repositories;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

namespace Project.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProjectContext _context;
    private ICompaniesRepository? _companyRepository;
    private IIndividualsRepository? _individualRepository;
    private ISoftwaresRepository? _softwareRepository;
    private IVersionsRepository? _versionsRepository;
    private ICategoriesRepository? _categoriesRepository;

    public UnitOfWork(ProjectContext context)
    {
        _context = context;
    }

    public ICompaniesRepository Companies => _companyRepository ??= new CompaniesRepository(_context);
    public IIndividualsRepository Individuals => _individualRepository ??= new IndividualsRepository(_context);
    public ISoftwaresRepository Softwares => _softwareRepository ??= new SoftwaresRepository(_context);
    public IVersionsRepository Versions => _versionsRepository ??= new VersionsRepository(_context);
    public ICategoriesRepository Categories => _categoriesRepository ??= new CategoriesRepository(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}