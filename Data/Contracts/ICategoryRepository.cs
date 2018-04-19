using System.Threading.Tasks;
using Core;
using Entities.Models;

namespace Contracts
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        Task<ResponseObject<object>> GetCategoryList(string userId);
    }
}
