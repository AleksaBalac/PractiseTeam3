using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Core;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels;

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

        public async Task<ResponseObject<object>> GetCategoryList(string userId)
        {
            var response = new ResponseObject<object>();
            try
            {
                var companyAccount = await AppDbContext.CompanyAccount
                                            .Include(a => a.Company)
                                                .ThenInclude(a => a.Categories)
                                                    .FirstOrDefaultAsync(a => a.UserId == userId);

                if (companyAccount == null)
                {
                    response.Success = false;
                    response.Message = "Can't find logged in company!";
                    return response;
                }

                var categoryListViewModel = new List<CategoryViewModel>();

                foreach (var category in companyAccount.Company.Categories)
                {
                    var categoryViewModel = new CategoryViewModel
                    {
                        Name = category.Name,
                        CategoryId = category.CategoryId
                    };

                    categoryListViewModel.Add(categoryViewModel);
                }

                response.Data = categoryListViewModel;
                response.Success = true;
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
