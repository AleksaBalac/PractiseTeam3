using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.JWT;
using Api.JWT.Helpers;
using Contracts;
using Core;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ViewModels;

namespace Api.Controllers
{
    [Route("api/")]
    public class AccountController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly AppDbContext _appDbContext;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ClaimsPrincipal _caller;

        public AccountController(IRepositoryWrapper repositoryWrapper, UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryWrapper = repositoryWrapper;
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _appDbContext = appDbContext;
            _jwtOptions = jwtOptions.Value;
            _caller = httpContextAccessor.HttpContext.User;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(loginViewModel.Email, loginViewModel.Password);
            
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var userId = identity.Claims.FirstOrDefault(a => a.Type == "id")?.Value;

            var user = _appDbContext.Users.FirstOrDefault(a => a.Id == userId);

            var result = await _userManager.AddClaimsAsync(user, identity.Claims);

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, loginViewModel.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        [HttpGet("user")]
        public IActionResult Home()
        {
            // retrieve the user info
            //HttpContext.User
            var handler = new JwtSecurityTokenHandler();

            var someData = Request.Headers["Authorization"].ToArray();

            JwtSecurityToken tokenS = handler.ReadToken(someData[0]) as JwtSecurityToken;

            var id = tokenS.Claims.FirstOrDefault(a=>a.Type == "id").Value;

            //var userId = _caller.Claims.Single(c => c.Type == tokenS.Id);
            var customer = _appDbContext.Users.FirstOrDefault(c => c.Id == id);

            
                return new OkObjectResult(new
                {
                    Message = "This is secure API and user data!",
                    customer.FirstName,
                    customer.LastName
                });
        }

        // POST api/accounts
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var userIdentity = _mapper.Map<AppUser>(model);

            var userIdentity = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                EmailConfirmed = true,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }

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
    }
}
