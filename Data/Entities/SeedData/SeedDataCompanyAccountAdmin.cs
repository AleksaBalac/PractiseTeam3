using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Entities.SeedData
{
    public static class SeedDataCompanyAccountAdmin
    {
        public static async Task<User> SeedCompanyAccountAdmin(UserManager<User> userManager)
        {
            if (!userManager.Users.Any(a => a.Email == "companyuser1@hotmail.com"))
            {
                var companyUser = new User
                {
                    FirstName = "Company",
                    LastName = "User",
                    UserName = "companyuser1@hotmail.com",
                    Email = "companyuser1@hotmail.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };

                var result = await userManager.CreateAsync(companyUser, "Pass1234");
                if (result.Succeeded)
                {
                    await AddToRoleAsync(companyUser, userManager);
                }

                return companyUser;
                
            }

            return null;
        }

        private static async Task<object> AddToRoleAsync(User companyUser, UserManager<User> userManager)
        {
            return await userManager.AddToRoleAsync(companyUser, "CompanyAdmin");
        }
    }
}

