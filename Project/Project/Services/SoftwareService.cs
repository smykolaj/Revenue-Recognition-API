using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.Exceptions;
using Project.Models;
using Project.Services.Interfaces;
using Version = Project.Models.Version;

namespace Project.Services;

public class SoftwareService : ISoftwareService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SoftwareService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SoftwareGetDto> AddSoftware(SoftwarePostDto dto)
    {
        if (!await _unitOfWork.Categories.ExistsById(dto.IdCategory))
            throw new DoesntExistException("category", "idCategory");

        Software newSoftware = _mapper.Map(dto);
        SoftwareGetDto returnDto = _mapper.Map( await _unitOfWork.Softwares.AddSoftware(newSoftware));
        return returnDto;

    }

    public async Task<CategoryGetDto> AddCategory(CategoryPostDto dto)
    {
        if (await _unitOfWork.Categories.ExistsByName(dto.CategoryName))
            throw new DoesntExistException("category", "idCategory");

        Category newCategory = new Category { CategoryName = dto.CategoryName };
        newCategory =  await _unitOfWork.Categories.AddCategory(newCategory);
        CategoryGetDto returnDto = new CategoryGetDto
            { CategoryName = newCategory.CategoryName, IdCategory = newCategory.IdCategory };
        return returnDto;
    }

    public async Task<VersionGetDto> AddSoftwareVersion(VersionPostDto dto, long idSoftware)
    {
        if (!await _unitOfWork.Softwares.ExistsById(idSoftware))
            throw new DoesntExistException("software", "idSoftware");

        Version newVersion = _mapper.Map(dto, idSoftware);
        VersionGetDto returnDto = _mapper.Map( await _unitOfWork.Versions.AddVersion(newVersion));
        return returnDto;
    }
}