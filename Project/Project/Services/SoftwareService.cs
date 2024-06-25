using Project.DTOs.Get;
using Project.DTOs.Post;
using Project.Exceptions;
using Project.Models;
using Project.Services.Interfaces;

namespace Project.Services;

public class SoftwareService : ISoftwareService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly Mapper _mapper;

    public SoftwareService(IUnitOfWork unitOfWork, Mapper mapper)
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

    public Task<VersionGetDto> AddSoftwareVersion(VersionPostDto dto)
    {
        throw new NotImplementedException();
    }
}