using Project.DTOs.Post;
using Version = Project.Models.Version;

namespace Project.Repositories.Interfaces;

public interface IVersionsRepository
{
    Task<Version> AddVersion(Version newVersion);
    Task<bool> ExistsById(long dtoIdVersion);
    Task<bool> VersionIsAssociatedWith(long dtoIdVersion, long dtoIdSoftware);
}