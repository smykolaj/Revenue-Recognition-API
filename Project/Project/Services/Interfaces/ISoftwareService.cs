using Project.DTOs.Get;
using Project.DTOs.Post;

namespace Project.Services.Interfaces;

public interface ISoftwareService
{
    Task<SoftwareGetDto> AddSoftware(SoftwarePostDto dto);
    Task<CategoryGetDto> AddCategory(CategoryPostDto dto);
    Task<VersionGetDto> AddSoftwareVersion(VersionPostDto dto, long idSoftware);
}