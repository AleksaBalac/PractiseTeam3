using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Entities.Models
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
                //await CreateRoles();

                await CreateCompanyAccountAsync();


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

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task CreateCompanyAccountAsync()
        {
            CompanyAccount companyAccount = null;
            Random random = new Random();

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
                companyAccount = new CompanyAccount
                {
                    Company = new Company
                    {
                        Name = "Company1",
                        Address = "Some Address",
                        ContactPerson = "Name",
                        ValidLicenceTill = DateTime.Now.AddMonths(7),
                        CompanyId = Guid.NewGuid().ToString(),
                        Categories = new List<Category>()
                        {
                            new Category
                            {
                                Name = "Category1",
                                Items = new List<InventoryItem>()
                                {
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 1,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 2,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 3,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 4,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 5,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 6,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 7,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 8,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 9,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 10,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 11,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 12,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 13,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 14,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 15,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 16,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 17,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 18,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 19,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 10,
                                        Value = random.Next(1, 100)
                                    }
                                }
                            },
                            new Category
                            {
                                Name = "Category2",
                                Items = new List<InventoryItem>()
                                {
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 1,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 2,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 3,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 4,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 5,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 6,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 7,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 8,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 9,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 10,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 11,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 12,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 13,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 14,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 15,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 16,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 17,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 18,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 19,
                                        Value = random.Next(1, 100)
                                    },
                                    new InventoryItem()
                                    {
                                        BarCode = Guid.NewGuid().ToString(),
                                        Description =
                                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam consequat, diam ac pretium accumsan, mi turpis dictum dui, sed vulputate neque lacus vel arcu. ",
                                        OrderNumber = 10,
                                        Value = random.Next(1, 100)
                                    }
                                }
                            }
                        }
                    },
                    UserId = companyUser?.Id
                };
            }

            if (companyAccount != null) await _appDbContext.CompanyAccount.AddAsync(companyAccount);

            await _appDbContext.SaveChangesAsync();
        }

        private async Task CreateRoles()
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

        }
    }
}
