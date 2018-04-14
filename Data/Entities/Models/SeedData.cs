using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Entities.Models
{
    public class SeedData
    {
        private readonly AppDbContext _appDbContext;

        public SeedData(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async void SeedAdminUser()
        {
            try
            {
                var user = new User
                {
                    FirstName = "App",
                    LastName = "User",
                    UserName = "AppUser",
                    Email = "appuser@hotmail.com",
                    NormalizedEmail = "appuser@hotmail.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var roleStore = new RoleStore<IdentityRole>(_appDbContext);

                if (!_appDbContext.Roles.Any(r => r.Name == "SuperAdmin"))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = "SuperAdmin", NormalizedName = "SUPERADMIN" });
                }

                if (!_appDbContext.Roles.Any(r => r.Name == "CompanyAdmin"))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = "CompanyAdmin", NormalizedName = "COMPANYADMIN" });
                }

                if (!_appDbContext.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<User>();
                    var hashed = password.HashPassword(user, "P@ss1234");
                    user.PasswordHash = hashed;
                    var userStore = new UserStore<User>(_appDbContext);
                    await userStore.CreateAsync(user);
                    await userStore.AddToRoleAsync(user, "CompanyAdmin");
                }

                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
