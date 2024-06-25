using Project.DTOs.Post;
using Version = Project.Models.Version;

namespace Project.Repositories.Interfaces;

public interface IVersionsRepository
{
    Task<Version> AddVersion(Version newVersion);
}