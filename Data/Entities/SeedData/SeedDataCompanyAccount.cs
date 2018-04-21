using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Entities.SeedData
{
    public static class SeedDataCompanyAccount
    {
        public static async Task<bool> SeedCompanyAccountAsync(AppDbContext appDbContext, UserManager<User> userManager)
        {
            try
            {
                CompanyAccount companyAccount = null;

                User companyUser = await SeedDataCompanyAccountAdmin.SeedCompanyAccountAdmin(userManager);
                

                if (!appDbContext.CompanyAccount.Any())
                {
                    Company company = new Company
                    {
                        CompanyId = Guid.NewGuid().ToString(),
                        Name = "Company1",
                        Address = "Some Address",
                        ContactPerson = "Name and last name",
                        ValidLicenceTill = DateTime.Now.AddMonths(7),
                        Categories = GenerateCategoryList() //this also generate items for each category
                    };

                    companyAccount = new CompanyAccount
                    {
                        UserId = companyUser?.Id,
                        CompanyAccountId = Guid.NewGuid().ToString(),
                        CompanyId = company.CompanyId
                    };
                }

                if (companyAccount != null) appDbContext.CompanyAccount.Add(companyAccount);

                //await appDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private static ICollection<Category> GenerateCategoryList()
        {
            List<Category> categoryList = new List<Category>();

            for (int i = 1; i < 4; i++)
            {
                Category category = new Category
                {
                    CategoryId = Guid.NewGuid().ToString(),
                    Name = $"Category{i}",
                    Items = GenerateItemList()
                };

                categoryList.Add(category);
            }

            return categoryList;
        }

        private static ICollection<InventoryItem> GenerateItemList()
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
                    Value = random.Next(1, 1000)
                };

                inventoryItems.Add(inventoryItem);
            }


            return inventoryItems;
        }

    }
}