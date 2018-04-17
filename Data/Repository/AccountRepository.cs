using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

            result.Data = jwt;
            result.Message = "User is successfully logged in!";
            
            return result;
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
