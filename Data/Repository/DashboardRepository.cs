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
using Newtonsoft.Json.Linq;
using ViewModels;

namespace Repository
{
    public class DashboardRepository : RepositoryBase<User>, IDashboardRepository
    {
        private readonly UserManager<User> _userManager;

        public DashboardRepository(AppDbContext appDbContext, UserManager<User> userManager)
            : base(appDbContext)
        {
            _userManager = userManager;
        }

        public async Task<ResponseObject<object>> GetCompanyDetails(string userId)
        {
            var response = new ResponseObject<object>();

            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var inRole = await _userManager.GetRolesAsync(user);

                if (!inRole[0].Contains("SuperAdmin"))
                {
                    response.StatusCode = StatusCode.Ok;
                    response.Success = false;
                    response.Data = null;
                    return response;
                }

                JArray jArray = new JArray();

                var companies = await AppDbContext.Companies
                    .Include(a => a.Categories)
                        .ThenInclude(a => a.Items)
                            .OrderBy(a => a.Name)
                                .ToListAsync();

                foreach (var company in companies)
                {
                    var categories = company.Categories.OrderBy(a => a.Name).ToList();

                    var items = 0;

                    categories.ForEach(category => { items += category.Items.Count; });

                    JObject jObject = new JObject
                    {
                        {"companyName", company.Name },
                        {"companyCategories", categories.Count },
                        {"companyItems", items }
                    };

                    jArray.Add(jObject);
                }

                response.Data = jArray;
                response.StatusCode = StatusCode.Ok;
                response.Success = true;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCode.BadRequest;
                response.Message = e.Message;
                return response;
            }
        }

        public async Task<ResponseObject<object>> GetCategoryDetails(string userId)
        {
            var response = new ResponseObject<object>();

            try
            {
                List<Category> categories;

                var user = await _userManager.FindByIdAsync(userId);
                var inRole = await _userManager.GetRolesAsync(user);

                if (inRole[0].Contains("SuperAdmin"))
                {
                    categories = await AppDbContext.Categories
                            .Include(a => a.Items)
                                .OrderBy(a => a.Name)
                                    .ToListAsync();
                }
                else
                {
                    var companyAccount = await AppDbContext.CompanyAccount
                        .Include(a => a.Company)
                            .FirstOrDefaultAsync(a => a.UserId == userId);

                    categories = await AppDbContext.Categories
                            .Include(a => a.Items)
                                .Where(a => a.CompanyId == companyAccount.Company.CompanyId)
                                    .OrderBy(a => a.Name)
                                        .ToListAsync();
                }

                JArray jArray = new JArray();

                foreach (var category in categories)
                {
                    JObject jObject = new JObject
                    {
                        {"categoryName", category.Name },
                        {"items", category.Items.Count }
                    };

                    jArray.Add(jObject);
                }


                response.Data = jArray;
                response.StatusCode = StatusCode.Ok;
                response.Success = true;

                return response;
            }
            catch (Exception e)
            {
                response.StatusCode = StatusCode.BadRequest;
                response.Message = e.Message;
                return response;
            }
        }

        public Task<ResponseObject<object>> GetItemDetails(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
