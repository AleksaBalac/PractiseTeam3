using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Core;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace Repository
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        private readonly UserManager<User> _userManager;

        public UsersRepository(AppDbContext appDbContext, UserManager<User> userManager) : base(appDbContext)
        {
            _userManager = userManager;
        }

        public ResponseObject<object> GetUsersList(string userId)
        {
            var response = new ResponseObject<object>();
            try
            {
                //var users = AppDbContext.CompanyAccount.Where(a => a.UserId == userId)?.Select(a => a.User).ToList();

                var company = AppDbContext.CompanyAccount.Include(a=>a.Company).FirstOrDefault(a => a.UserId == userId)?.Company;

                var users = AppDbContext.CompanyAccount.Include(a => a.User)
                    .Where(a => a.CompanyId == company.CompanyId).Select(a => a.User).ToList();
                
                //var userRole = AppDbContext.UserRoles.FirstOrDefault(a => a.UserId == userId);
                //var role = AppDbContext.Roles.FirstOrDefault(a => a.Id == userRole.RoleId)?.Name;

                //Task<IList<User>> users = _userManager.GetUsersInRoleAsync(role);

                var usersViewModel = new List<UsersViewModel>();

                foreach (var user in users)
                {
                    Task<IList<string>> role = _userManager.GetRolesAsync(user);

                    var userViewModel = new UsersViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Role = role.Result[0]
                    };

                    usersViewModel.Add(userViewModel);
                }

                response.Data = usersViewModel;
                return response;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<ResponseObject<object>> SaveNewUserAsync(string userId, UsersViewModel usersViewModel)
        {
            var response = new ResponseObject<object>();
            try
            {
                //find company
                var company = AppDbContext.CompanyAccount
                            .Include(a => a.Company)
                                .FirstOrDefault(a => a.UserId == userId)?
                                    .Company;

                if (company == null)
                {
                    response.Message = "Can not find company";
                    response.Success = false;
                    return response;
                }

                //create new user with role
                var user = new User
                {
                    FirstName = usersViewModel.FirstName,
                    LastName = usersViewModel.LastName,
                    Email = usersViewModel.Email,
                    UserName = usersViewModel.Email,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(user, "Pass1234");//TODO change default password with email registration
                await _userManager.AddToRoleAsync(user, usersViewModel.Role ?? "CompanyAdmin");

                //create account company
                var companyAccount = new CompanyAccount
                {
                    CompanyAccountId = Guid.NewGuid().ToString(),
                    UserId = user.Id,
                    CompanyId = company?.CompanyId
                };

                await AppDbContext.CompanyAccount.AddAsync(companyAccount);
                await AppDbContext.SaveChangesAsync();
                
                response.Success = true;
                response.Data = null;
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ResponseObject<object> GetListOfRoles()
        {
            var response = new ResponseObject<object>();

            try
            {
                response.Data = AppDbContext.Roles.ToList();
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
