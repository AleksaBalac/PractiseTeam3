using System;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

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
                var rolesCreated = await SeedDataRoles.SeedRoles(_appDbContext);
                if (rolesCreated)
                {
                    var companyAccountCreated = await SeedDataCompanyAccount.SeedCompanyAccountAsync(_appDbContext, _userManager);
                    if (companyAccountCreated) await CreateSuperAdminAndSave();
                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task CreateSuperAdminAndSave()
        {
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

            await _appDbContext.SaveChangesAsync();
        }
    }

}
