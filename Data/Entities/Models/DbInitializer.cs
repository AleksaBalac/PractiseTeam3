using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Entities.Models
{
    public class DbInitializer
    {
        protected async Task Seed(AppDbContext context)
        {
            if (!context.Roles.Any(a => a.Name == "SuperAdmin"))
            {
                var role = new IdentityRole()
                {
                    Name = "SuperAdmin"
                };

                await context.Roles.AddAsync(role);
            }

            if (!context.Roles.Any(a => a.Name == "CompanyAdmin"))
            {
                var role = new IdentityRole()
                {
                    Name = "CompanyAdmin"
                };

                await context.Roles.AddAsync(role);
            }

        }
    }
}
