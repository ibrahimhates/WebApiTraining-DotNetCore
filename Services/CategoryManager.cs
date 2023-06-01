using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryManager(IRepositoryManager manager)
        {
            _manager=manager;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
        {
            var categories = await _manager.Category.GetAllCategoriesAsync(trackChanges);
            return categories;
        }

        public async Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges)
        {
            var category = await GetOneCategoryByIdAndCheckExists(id,trackChanges);
            return category;
        }

        public async Task<Category> CreateOneCategoryAsync(Category category)
        {
            _manager.Category.CreateOneCategory(category);
            await _manager.SaveAsync();
            return category;
        }

        public async Task UpdateOneCategoryAsync(int id, Category category, bool trackChanges)
        {
            var entity = await GetOneCategoryByIdAndCheckExists(id,trackChanges);

            entity.CategoryName = category.CategoryName;

            _manager.Category.UpdateOneCategory(entity);
            await _manager.SaveAsync();
        }

        public async Task DeleteOneCategoryAsync(int id, bool trackChanges)
        {
            var category = await GetOneCategoryByIdAndCheckExists(id, trackChanges);
            _manager.Category.DeleteOneCategory(category);
            await _manager.SaveAsync();
        }

        private async Task<Category> GetOneCategoryByIdAndCheckExists(int id, bool trackChanges)
        {
            // check entity 
            var category = await _manager.Category.GetByIdAsync(id, trackChanges);
            if (category is null)
                throw new CategoriesNotFoundException(id);

            return category;
        }

    }
}
