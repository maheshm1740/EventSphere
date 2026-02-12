using DLEventSphere.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLEventSphere.Abstract
{
    public interface ICategory
    {
        Task<Category> CreateCategoryAsync(string name);

        Task<List<Category>> GetAllCategories();

        Task<Category> GetCategoryById(int id);

        Task<bool> DeleteCategory(int id);
    }
}
