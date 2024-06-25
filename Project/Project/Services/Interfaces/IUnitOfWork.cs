using Project.Repositories.Interfaces;

namespace Project.Services.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICompaniesRepository Companies { get; }
    IIndividualsRepository Individuals { get; }
    ISoftwaresRepository Softwares { get; }
    IVersionsRepository Versions { get; }
    ICategoriesRepository Categories { get; }
    Task<int> CompleteAsync();
}