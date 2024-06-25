using Project.Models;

namespace Project.Repositories.Interfaces;

public interface ICategoriesRepository
{
    Task<bool> ExistsById(long dtoIdCategory);
    Task<bool> ExistsByName(string dtoCategoryName);
    Task<Category> AddCategory(Category newCategory);
}