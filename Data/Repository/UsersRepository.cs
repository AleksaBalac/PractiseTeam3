using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ResponseObject<object>> GetUsersList(string userId)
        {
            var response = new ResponseObject<object>();

            try
            {
                List<User> users = new List<User>();

                //check who is logged in 
                var loggedInUser = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == userId);

                if (loggedInUser == null)
                {
                    response.Message = "User not found!";
                    response.StatusCode = StatusCode.BadRequest;
                    return response;
                }

                IList<string> userInRole = await _userManager.GetRolesAsync(loggedInUser);

                if (userInRole.Any(a => a.Contains("SuperAdmin")))
                {
                    //return all users from database 
                    users = await _userManager.Users.ToListAsync();
                }

                if (userInRole.Any(a => a.Contains("CompanyAdmin")))
                {
                    var company = AppDbContext.CompanyAccount
                              .Include(a => a.Company)
                                    .FirstOrDefault(a => a.UserId == userId)?.Company;

                    users = AppDbContext.CompanyAccount
                            .Include(a => a.User)
                                .Where(a => a.CompanyId == company.CompanyId)
                                       .Select(a => a.User).ToList();

                }

                var usersViewModel = new List<UsersViewModel>();

                foreach (var user in users)
                {
                    if (user.Id == userId) continue;//skip logged in user

                    IList<string> role = await _userManager.GetRolesAsync(user);

                    var userViewModel = new UsersViewModel
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Role = role.Count != 0 ? role[0] : ""
                    };

                    usersViewModel.Add(userViewModel);
                }

                response.Data = usersViewModel;
                response.StatusCode = StatusCode.Ok;
                return response;

            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }
        }

        public async Task<ResponseObject<object>> SaveNewUserAsync(string userId, UsersViewModel usersViewModel)
        {
            var response = new ResponseObject<object>();
            try
            {
                // first check which role is selected
                // super admin don't need to be stored to CompanyAccount
                // company admin must compnay
                // user ??

                //find company
                var company = AppDbContext.CompanyAccount
                            .Include(a => a.Company)
                                .FirstOrDefault(a => a.UserId == userId)?
                                    .Company;

                if (company == null)
                {
                    response.Message = "Can not find company";
                    response.Success = false;
                    response.StatusCode = StatusCode.BadRequest;
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

                CompanyAccount companyAccount = null;

                if (usersViewModel.Role != "User")
                {

                    //create account company
                    companyAccount = new CompanyAccount
                    {
                        CompanyAccountId = Guid.NewGuid().ToString(),
                        UserId = user.Id,
                        CompanyId = company?.CompanyId
                    };
                }

                if (companyAccount != null) await AppDbContext.CompanyAccount.AddAsync(companyAccount);

                await _userManager.CreateAsync(user, "Pass1234");//TODO change default password with email registration
                await _userManager.AddToRoleAsync(user, usersViewModel.Role ?? "CompanyAdmin");

                await AppDbContext.SaveChangesAsync();

                response.Success = true;
                response.StatusCode = StatusCode.Ok;
                response.Message = "User is added successfully!";
                response.Data = null;
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }
        }

        public async Task<ResponseObject<object>> GetListOfRoles(string userId)
        {
            var response = new ResponseObject<object>();

            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == userId);

                if (user == null)
                {
                    response.StatusCode = StatusCode.BadRequest;
                    response.Message = "Can't find user!";
                    return response;
                }

                IList<string> userRole = await _userManager.GetRolesAsync(user);

                var dbRoles = AppDbContext.Roles.ToList();

                List<IdentityRole> roles = new List<IdentityRole>();

                if (userRole.Contains("SuperAdmin"))
                {
                    roles.Add(dbRoles.FirstOrDefault(a => a.Name.Contains("SuperAdmin")));
                    roles.Add(dbRoles.FirstOrDefault(a => a.Name.Contains("CompanyAdmin")));
                    roles.Add(dbRoles.FirstOrDefault(a => a.Name.Contains("User")));
                }

                if (userRole.Contains("CompanyAdmin"))
                {
                    roles.Add(dbRoles.FirstOrDefault(a => a.Name.Contains("CompanyAdmin")));
                    roles.Add(dbRoles.FirstOrDefault(a => a.Name.Contains("User")));
                }

                if (userRole.Contains("User"))
                {
                    //?
                    roles.Add(dbRoles.FirstOrDefault(a => a.Name.Contains("User")));
                }

                response.Data = roles;
                response.StatusCode = StatusCode.Ok;
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }
        }

        public async Task<ResponseObject<object>> UpdateUserAsync(UsersViewModel userViewModel)
        {
            var response = new ResponseObject<object>();
            try
            {
                var user = _userManager.Users.FirstOrDefault(a => a.Id == userViewModel.Id);
                if (user == null)
                {
                    response.Message = "Can't find user";
                    response.Success = false;
                    response.StatusCode = StatusCode.BadRequest;
                    return response;
                }

                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Email = userViewModel.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    bool isInRole = await _userManager.IsInRoleAsync(user, userViewModel.Role);
                    if (isInRole)
                    {
                        //TODO discuss what to do if user have another roles
                        var removed = await _userManager.RemoveFromRolesAsync(user, new List<string> { "SuperAdmin", "CompanyAdmin", "User" });
                        var addToNewRole = await _userManager.AddToRoleAsync(user, userViewModel.Role);
                    }
                }

                response.Data = user;
                response.Success = true;
                response.StatusCode = StatusCode.Ok;
                response.Message = "User successfully updated!";
                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }
        }

        public async Task<ResponseObject<object>> DeleteUserAsync(string userId)
        {
            var response = new ResponseObject<object>();

            try
            {
                var user = _userManager.Users.FirstOrDefault(a => a.Id == userId);
                if (user == null)
                {
                    response.Message = "Can't find user!";
                    response.Success = false;
                    response.StatusCode = StatusCode.BadRequest;
                    return response;
                }

                //first delete user from companyAccount
                var companyAccount = await AppDbContext.CompanyAccount.FirstOrDefaultAsync(a => a.UserId == user.Id);
                if (companyAccount == null)
                {
                    response.Message = "Can't find companyAccount!";
                    response.Success = false;
                    response.StatusCode = StatusCode.BadRequest;
                    return response;
                }

                AppDbContext.CompanyAccount.Remove(companyAccount);

                //now from user
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    response.Message = "User is successfully deleted!";
                    response.StatusCode = StatusCode.Ok;
                    response.Success = true;
                }

                await AppDbContext.SaveChangesAsync();


                return response;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.StatusCode = StatusCode.BadRequest;
                return response;
            }
        }
    }
}
