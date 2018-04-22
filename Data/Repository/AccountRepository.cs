using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts;
using Core;
using Core.JWT;
using Core.JWT.Helpers;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ViewModels;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Category>, IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public AccountRepository(AppDbContext appDbContext, UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
            : base(appDbContext)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<ResponseObject<object>> Login(LoginViewModel loginViewModel)
        {
            ResponseObject<object> result = new ResponseObject<object>();

            if (loginViewModel.Email == null || loginViewModel.Password == null)
            {
                result.Data = null;
                result.StatusCode = StatusCode.Unauthorized;
                result.Message = "Can not find email or password!";
                return result;
            }

            var identity = await GetClaimsIdentity(loginViewModel.Email, loginViewModel.Password);

            if (identity == null)
            {
                //return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
                result.Data = null;
                result.StatusCode = StatusCode.Unauthorized;
                result.Message = "Can't find user!";
                return result;
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, loginViewModel.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            var token = JsonConvert.DeserializeObject(jwt);

            result.Data = token;
            result.StatusCode = StatusCode.Ok;
            result.Message = "User is successfully logged in!";

            return result;
        }

        public async Task<ResponseObject<object>> GetUserDetails(string userId)
        {
            ResponseObject<object> result = new ResponseObject<object>();

            try
            {
                var user = AppDbContext.Users.FirstOrDefault(c => c.Id == userId);

                if (user == null)
                {
                    result.Data = null;
                    result.Message = "Can't find user!";
                    result.StatusCode = StatusCode.BadRequest;

                    return result;
                }

                var companyAccount = AppDbContext.CompanyAccount.Include(a => a.Company)?.FirstOrDefault(a => a.UserId == user.Id);
                var roles = await _userManager.GetRolesAsync(user);

                JObject jUser = new JObject()
                {
                    {"fullName", user?.FullName},
                    {"companyName", companyAccount?.Company.Name},
                    {"userRole", roles.FirstOrDefault()}
                };

                //UsersViewModel usersViewModel = new UsersViewModel
                //{
                //    FirstName = user.FirstName,
                //    LastName = user.LastName,
                //    Email = user.Email,
                //    Id = user.Id,
                //    Role = roles[0]
                //};

                result.Data = jUser;
                result.StatusCode = StatusCode.Ok;

                return result;
            }

            catch (Exception e)
            {
                //TODO add log
                result.Data = null;
                result.Message = e.Message;
                result.StatusCode = StatusCode.BadRequest;

                return result;
            }
        }

        public async Task<ResponseObject<object>> Register(RegistrationViewModel registrationViewModel)
        {
            var response = new ResponseObject<object>();

            try
            {
                var user = new User
                {
                    FirstName = registrationViewModel.FirstName,
                    LastName = registrationViewModel.LastName,
                    Email = registrationViewModel.Email,
                    UserName = registrationViewModel.Email,
                    EmailConfirmed = true
                };

                var company = new Company
                {
                    Name = registrationViewModel.Name,
                    CompanyId = Guid.NewGuid().ToString()
                };

                var companyAccount = new CompanyAccount
                {
                    CompanyAccountId = Guid.NewGuid().ToString(),
                    CompanyId = company.CompanyId,
                    UserId = user.Id
                };

                var result = await _userManager.CreateAsync(user, registrationViewModel.Password);
                if (result.Succeeded) AccountConfirm(user);

                var role = _userManager.AddToRoleAsync(user, "CompanyAdmin");

                await AppDbContext.Companies.AddAsync(company);
                await AppDbContext.CompanyAccount.AddAsync(companyAccount);
                await AppDbContext.SaveChangesAsync();

                response.Data = null;
                response.Message = "Successfully registered! Check your email for activation!";
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

        private async void AccountConfirm(User user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            AppDomain root = AppDomain.CurrentDomain;

            var callbackUrl = $"{root}/api/account/userId=${user.Id}&code=${code}";

            //var callbackUrl = Url.Action(
            //    "ConfirmEmail", "Account",
            //    new { userId = user.Id, code = code },
            //    protocol: Request.Url.Scheme);



            //await _userManager.SendEmailAsync(user.Id,
            //    "Confirm your account",
            //    "Please confirm your account by clicking this link: <a href=\""
            //    + callbackUrl + "\">link</a>");
            // ViewBag.Link = callbackUrl;   // Used only for initial demo.
        }

        #region private methods
        private async Task<ClaimsIdentity> GetClaimsIdentity(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByEmailAsync(email);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(email, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
        #endregion
    }
}
