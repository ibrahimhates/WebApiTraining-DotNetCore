
using Entities.DataTransferObject;
using Entities.Models;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetOneCategoryByIdAsync(int id,bool trackChanges);
        Task<Category> CreateOneCategoryAsync(Category category);
        Task UpdateOneCategoryAsync(int id,Category category, bool trackChanges);
        Task DeleteOneCategoryAsync(int id,bool trackChanges);

    }
}
