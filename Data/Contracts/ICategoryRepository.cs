using Entities.Models;

namespace Contracts
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
    }
}
