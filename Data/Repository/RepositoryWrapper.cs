using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public ICategoryRepository Category { get; set; }

        public RepositoryWrapper(AppDbContext repositoryContext)
        {
            this.Category = new CategoryRepository(repositoryContext);
        }
    }
}
