using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly ProjectContext _context;
    

    public CategoriesRepository(ProjectContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsById(long dtoIdCategory)
    {
        return await _context.Categories.AnyAsync(c => c.IdCategory.Equals(dtoIdCategory));
    }

    public async Task<bool> ExistsByName(string dtoCategoryName)
    {
        return await _context.Categories.AnyAsync(c => c.CategoryName.Equals(dtoCategoryName));
    }

    public async Task<Category> AddCategory(Category newCategory)
    {
        await _context.AddAsync(newCategory);
        await _context.SaveChangesAsync();
        return newCategory;
    }
}