using DLEventSphere.Abstract;
using DLEventSphere.DTO_s;
using DLEventSphere.Model;
using DLEventSphere.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEventSphere
{
    public class BlCategory
    {
        private readonly ICategory _repo;

        public BlCategory(ICategory repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResponse<Category>> CreateCategory(string name)
        {
            var response = new ServiceResponse<Category>();

            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Category name is required.");

                response.Data = await _repo.CreateCategoryAsync(name);
                response.Message = "Category created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Category>>> GetAllCategories()
        {
            var response = new ServiceResponse<List<Category>>();

            try
            {
                response.Data = await _repo.GetAllCategories();
                response.Message = "Categories retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<Category>> GetCategoryById(int id)
        {
            var response = new ServiceResponse<Category>();

            try
            {
                response.Data = await _repo.GetCategoryById(id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteCategory(int id)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                response.Data = await _repo.DeleteCategory(id);
                response.Message = "Category deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Data = false;
            }

            return response;
        }
    }
}
