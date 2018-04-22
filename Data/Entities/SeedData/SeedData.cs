using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Entities.SeedData
{
    public class SeedData
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;


        public SeedData(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public async void SeedAdminUser()
        {
            try
            {
                var roleStore = new RoleStore<IdentityRole>(_appDbContext);

                if (!_appDbContext.Roles.Any(r => r.Name == "SuperAdmin"))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = "SuperAdmin", NormalizedName = "SUPERADMIN" });
                }

                if (!_appDbContext.Roles.Any(r => r.Name == "CompanyAdmin"))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = "CompanyAdmin", NormalizedName = "COMPANYADMIN" });
                }

                if (!_appDbContext.Roles.Any(r => r.Name == "User"))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = "User", NormalizedName = "USER" });
                }
                    
                CompanyAccount companyAccount = null;
                User companyUser = null;

                if (!_userManager.Users.Any(a => a.Email == "companyuser1@hotmail.com"))
                {
                    companyUser = new User
                    {
                        FirstName = "Company",
                        LastName = "User",
                        UserName = "companyuser1@hotmail.com",
                        Email = "companyuser1@hotmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };

                    await _userManager.CreateAsync(companyUser, "Pass1234");
                    await _userManager.AddToRoleAsync(companyUser, "CompanyAdmin");

                }

                if (!_appDbContext.CompanyAccount.Any())
                {
                    var companyId = Guid.NewGuid().ToString();

                    Company company = new Company
                    {
                        CompanyId = companyId,
                        Name = "Company1",
                        Address = "Some Address",
                        ContactPerson = "Name and last name",
                        ValidLicenceTill = DateTime.Now.AddMonths(7),
                        Categories = GenerateCategoryList(companyId) //this also generate items for each category
                    };

                    _appDbContext.Companies.Add(company);

                    companyAccount = new CompanyAccount
                    {
                        CompanyAccountId = Guid.NewGuid().ToString(),
                        UserId = companyUser?.Id,
                        User = companyUser,
                        CompanyId = company.CompanyId,
                        Company = company
                    };
                }

                if (companyAccount != null) _appDbContext.CompanyAccount.Add(companyAccount);
                

                if (!_userManager.Users.Any(a => a.Email == "superadmin@hotmail.com"))
                {
                    var superAdmin = new User
                    {
                        FirstName = "Super",
                        LastName = "Admin",
                        UserName = "superadmin@hotmail.com",
                        Email = "superadmin@hotmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };


                    //create super admin
                    await _userManager.CreateAsync(superAdmin, "Pass1234");
                    await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                }

                _appDbContext.SaveChanges();
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private ICollection<Category> GenerateCategoryList(string companyId)
        {
            List<Category> categoryList = new List<Category>();

            for (int i = 1; i < 4; i++)
            {
                var categoryId = Guid.NewGuid().ToString();

                Category category = new Category
                {
                    CategoryId = categoryId,
                    Name = $"Category{i}",
                    Items = GenerateItemList(categoryId),
                    CompanyId = companyId
                };

                categoryList.Add(category);
            }

            _appDbContext.Categories.AddRange(categoryList);

            return categoryList;
        }

        private ICollection<InventoryItem> GenerateItemList(string categoryId)
        {
            Random random = new Random();

            List<InventoryItem> inventoryItems = new List<InventoryItem>();

            for (int i = 1; i < 41; i++)
            {
                InventoryItem inventoryItem = new InventoryItem
                {
                    Name = $"Item{i}",
                    InventoryItemId = Guid.NewGuid().ToString(),
                    BarCode = Guid.NewGuid().ToString(),
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                    OrderNumber = i,
                    Value = random.Next(1, 1000),
                    CategoryId = categoryId
                };

                inventoryItems.Add(inventoryItem);
            }

            _appDbContext.InventoryItems.AddRange(inventoryItems);

            return inventoryItems;
        }
    }

}
