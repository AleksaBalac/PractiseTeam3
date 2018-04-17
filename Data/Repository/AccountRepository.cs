using System;
using System.IdentityModel.Tokens.Jwt;
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

            var identity = await GetClaimsIdentity(loginViewModel.Email, loginViewModel.Password);

            if (identity == null)
            {
                //return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
                result.Data = null;
                result.Message = "Something is wrong on login";
                return result;
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, loginViewModel.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            var token = JsonConvert.DeserializeObject(jwt);

            result.Data = token;
            result.Message = "User is successfully logged in!";

            return result;
        }

        public async Task<ResponseObject<object>> GetUserDetails(string token)
        {
            ResponseObject<object> result = new ResponseObject<object>();

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenS = handler.ReadToken(token) as JwtSecurityToken;

            var id = tokenS?.Claims?.FirstOrDefault(a => a.Type == "id")?.Value;

            var user = AppDbContext.Users.FirstOrDefault(c => c.Id == id);
            var roles = await _userManager.GetRolesAsync(user);

            JObject jUser = new JObject()
            {
                {"FullName", user?.FullName},
                {"UserRole", roles.FirstOrDefault()}
            };
            
            result.Data = jUser;

            return result;

        }

        public async Task<ResponseObject<object>> Register(RegistrationViewModel registrationViewModel)
        {
            var response = new ResponseObject<object>();

            var user = new User
            {
                FirstName = registrationViewModel.FirstName,
                LastName = registrationViewModel.LastName,
                Email = registrationViewModel.Email,
                UserName = registrationViewModel.Email
            };

            var company = new Company
            {
                Name = registrationViewModel.Name,
                CompanyId = Guid.NewGuid().ToString()
            };

            //create company account

            var result = await _userManager.CreateAsync(user, registrationViewModel.Password);
            var role = _userManager.AddToRoleAsync(user, "CompanyAdmin");

            await AppDbContext.Companies.AddAsync(company);
            await AppDbContext.SaveChangesAsync();

            response.Data = null;
            response.Message = "Successfully registered! Check your email for activation!";

            return response;
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
