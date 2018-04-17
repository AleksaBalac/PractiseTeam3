using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }


        public void AddCategory(Category category)
        {
            Create(category);
            Save();
        }
    }
}
