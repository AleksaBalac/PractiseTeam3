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

        public async Task<ResponseObject<object>> AddCategory(string userId, CategoryViewModel categoryViewModel)
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
                    response.Message = "Can't find logged in user";
                    response.Success = false;
                    return response;
                }

                var category = new Category
                {
                    CategoryId = Guid.NewGuid().ToString(),
                    Name = categoryViewModel.Name
                };

                companyAccount.Company.Categories.Add(category);
                await AppDbContext.SaveChangesAsync();

                var categoryVm = new CategoryViewModel
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                };

                response.Data = categoryVm;
                response.Message = "Category successfully added!";
                
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseObject<object>> UpdateCategory(CategoryViewModel categoryViewModel)
        {
            var response = new ResponseObject<object>();
            try
            {
                var category = await AppDbContext.Categories.FirstOrDefaultAsync(a => a.CategoryId == categoryViewModel.CategoryId);

                if (category == null)
                {
                    response.Message = "Can't find logged category";
                    response.Success = false;
                    return response;
                }

                category.Name = categoryViewModel.Name;

                AppDbContext.Update(category);
                
                await AppDbContext.SaveChangesAsync();

                var categoryVm = new CategoryViewModel
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                };

                response.Data = categoryVm;
                response.Message = "Category successfully updated!";

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }

        public async Task<ResponseObject<object>> DeleteCategory(string categoryId)
        {
            var response = new ResponseObject<object>();
            try
            {
                var category = await AppDbContext.Categories.FirstOrDefaultAsync(a => a.CategoryId == categoryId);

                if (category == null)
                {
                    response.Message = "Can't find logged category";
                    response.Success = false;
                    return response;
                }
                
                AppDbContext.Remove(category);

                await AppDbContext.SaveChangesAsync();

                var categoryVm = new CategoryViewModel
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                };

                response.Data = categoryVm;
                response.Message = "Category successfully deleted!";

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
            }

            return response;
        }
    }
}
