using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Core;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly UserManager<User> _userManager;

        public CategoryRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
        }

        public CategoryRepository(AppDbContext appDbContext, UserManager<User> userManager)
            : base(appDbContext)
        {
            _userManager = userManager;
        }

        public async Task<ResponseObject<object>> GetCategoryList(string userId)
        {
            var response = new ResponseObject<object>();
            try
            {
                var user = await AppDbContext.Users.FirstOrDefaultAsync(a => a.Id == userId);

                if (user == null)
                {
                    response.Success = false;
                    response.StatusCode = StatusCode.BadRequest;
                    response.Message = "Can't find user!";
                    return response;
                }

                var role = await _userManager.GetRolesAsync(user);

                List<Category> categories = new List<Category>();

                if (role.Contains("SuperAdmin") || role.Contains("User"))
                {
                    categories = AppDbContext.Categories.OrderBy(a => a.Name).ToList();
                }

                if (role.Contains("CompanyAdmin"))
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

                    categories = companyAccount.Company.Categories.OrderBy(a => a.Name).ToList();
                }

                

                var categoryListViewModel = new List<CategoryViewModel>();

                foreach (var category in categories)
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
                response.Message = e.Message;
                response.Success = false;
                response.StatusCode = StatusCode.BadRequest;
                return response;
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
                response.StatusCode = StatusCode.BadRequest;
                return response;
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
                response.StatusCode = StatusCode.BadRequest;
                return response;
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
                response.StatusCode = StatusCode.Ok;
                response.Message = "Category successfully deleted!";

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Success = false;
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }

            return response;
        }
    }
}
