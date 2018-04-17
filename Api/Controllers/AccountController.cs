using System;
using System.Threading.Tasks;
using Contracts;
using Core;
using Microsoft.AspNetCore.Mvc;
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
                //TODO add log
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("user/details")]
        public Task<ResponseObject<object>> Details()
        {
            try
            {
                var token = Request.Headers["Authorization"];

                if (token == "null") return null;

                var result = _repositoryWrapper.Account.GetUserDetails(token);

                return result;
            }
            catch (Exception e)
            {
                //TODO add log
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("register")]
        public Task<ResponseObject<object>> Register([FromBody] RegistrationViewModel registrationViewModel)
        {
            try
            {
                var result = _repositoryWrapper.Account.Register(registrationViewModel);
                return result;
            }
            catch (Exception e)
            {
                //TODO add log
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
