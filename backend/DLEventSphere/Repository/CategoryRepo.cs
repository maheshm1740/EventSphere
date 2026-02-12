using DLEventSphere.Abstract;
using DLEventSphere.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Repository
{
    public class CategoryRepo : ICategory
    {
        private readonly EventContext _context;

        public CategoryRepo(EventContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(string name)
        {
            var exists = await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());

            if (exists)
                throw new InvalidOperationException("Category with the same name already exists.");

            var category = new Category { Name = name.Trim() };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                throw new InvalidOperationException($"Category with id {id} not found.");

            return category;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Events)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                return false;

            if (category.Events != null && category.Events.Any())
                throw new InvalidOperationException(
                    "Cannot delete category with associated events.");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
