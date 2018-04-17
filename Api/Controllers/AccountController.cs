using System;
using System.Collections.Generic;
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

        public AccountController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public Task<ResponseObject<object>> Login([FromBody]LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return null;
                }

                var result = _repositoryWrapper.Account.Login(loginViewModel);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }            
        }

        //[HttpGet("user")]
        //public IActionResult Home()
        //{
        //    // retrieve the user info
        //    //HttpContext.User
        //    var handler = new JwtSecurityTokenHandler();

        //    var someData = Request.Headers["Authorization"].ToArray();

        //    JwtSecurityToken tokenS = handler.ReadToken(someData[0]) as JwtSecurityToken;

        //    var id = tokenS.Claims.FirstOrDefault(a=>a.Type == "id").Value;

        //    //var userId = _caller.Claims.Single(c => c.Type == tokenS.Id);
        //    var customer = _appDbContext.Users.FirstOrDefault(c => c.Id == id);

            
        //        return new OkObjectResult(new
        //        {
        //            Message = "This is secure API and user data!",
        //            customer.FirstName,
        //            customer.LastName
        //        });
        //}

        

        
    }
}
