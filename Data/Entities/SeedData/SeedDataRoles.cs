using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Entities.SeedData
{
    public static class SeedDataRoles
    {
        //private readonly RoleManager<Roles> _roleManager;

        public static async Task<bool> SeedRoles(AppDbContext appDbContext)
        {
            var roleStore = new RoleStore<IdentityRole>(appDbContext);

            if (!appDbContext.Roles.Any(r => r.Name == "SuperAdmin"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "SuperAdmin", NormalizedName = "SUPERADMIN" });
            }

            if (!appDbContext.Roles.Any(r => r.Name == "CompanyAdmin"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "CompanyAdmin", NormalizedName = "COMPANYADMIN" });
            }

            if (!appDbContext.Roles.Any(r => r.Name == "User"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "User", NormalizedName = "USER" });
            }

            return true;
        }

        //public async Task SeedRoles()
        //{
        //    try
        //    {
        //        if (!_roleManager.RoleExistsAsync("SuperAdmin").Result)
        //        {
        //            var role = new Roles();
        //            role.Name = "SuperAdmin";
        //            await _roleManager.CreateAsync(role);
        //        }

        //        if (!_roleManager.RoleExistsAsync("CompanyAdmin").Result)
        //        {
        //            var role = new Roles();
        //            role.Name = "CompanyAdmin";
        //            await _roleManager.CreateAsync(role);
        //        }

        //        if (!_roleManager.RoleExistsAsync("User").Result)
        //        {
        //            var role = new Roles();
        //            role.Name = "User";
        //            await _roleManager.CreateAsync(role);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //}

    }
}