using System.Threading.Tasks;
using Core;
using Entities.Models;
using ViewModels;

namespace Contracts
{
    public interface ICategoryRepository
    {
        Task<ResponseObject<object>> GetCategoryList(string userId);
        Task<ResponseObject<object>> AddCategory(string userId, CategoryViewModel category);
        Task<ResponseObject<object>> UpdateCategory(CategoryViewModel category);
        Task<ResponseObject<object>> DeleteCategory(string categoryId);
    }
}
