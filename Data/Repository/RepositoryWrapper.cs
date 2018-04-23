using Contracts;
using Core.JWT;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        public ICategoryRepository Category { get; set; }
        public IAccountRepository Account { get; set; }
        public IUsersRepository Users { get; set; }
        public IItemRepository Items { get; set; }
        public ICompanyRepository Company { get; set; }

        public RepositoryWrapper(AppDbContext repositoryContext, UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            this.Category = new CategoryRepository(repositoryContext, userManager);
            this.Account = new AccountRepository(repositoryContext, userManager, jwtFactory, jwtOptions);
            this.Users = new UsersRepository(repositoryContext, userManager);
            this.Items = new ItemRepository(repositoryContext);
            this.Company = new CompanyRepository(repositoryContext);
        }
    }
}
