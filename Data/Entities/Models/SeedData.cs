using System;
using System.Collections.Generic;
using System.Linq;
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
                //if (! await _roleManager.RoleExistsAsync("SuperAdmin"))
                //{
                //    var role = new IdentityRole("SuperAdmin");
                //    await _roleManager.CreateAsync(role);
                //}

                //if (!await _roleManager.RoleExistsAsync("CompanyAdmin"))
                //{
                //    var role = new IdentityRole("CompanyAdmin");
                //    await _roleManager.CreateAsync(role);
                //}

                //if (!await _roleManager.RoleExistsAsync("User"))
                //{
                //    var role = new IdentityRole("User");
                //    await _roleManager.CreateAsync(role);
                //}


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

                roleStore.Dispose();

                User user = null;

                if (!_userManager.Users.Any())
                {
                    user = new User
                    {
                        FirstName = "App",
                        LastName = "User",
                        UserName = "appuser@hotmail.com",
                        Email = "appuser@hotmail.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };
                }
                
                CompanyAccount accountCompany = null;
                Random random = new Random();

                if (!_appDbContext.CompanyAccount.Any())
                {
                    accountCompany = new CompanyAccount();
                    accountCompany.Company = new Company
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
                    };

                    accountCompany.User = user;
                    accountCompany.UserId = user.Id;
                }

                if (accountCompany != null) await _appDbContext.CompanyAccount.AddAsync(accountCompany);

                if (user != null)
                {
                    await _userManager.CreateAsync(user, "Pass1234");
                    await _userManager.AddToRoleAsync(user, "CompanyAdmin");
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
