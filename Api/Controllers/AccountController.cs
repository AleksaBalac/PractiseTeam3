using System;
using System.Threading.Tasks;
using Contracts;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using ViewModels;

namespace Api.Controllers
{
    [Route("api/")]
    public class AccountController : BaseController
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AccountController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            try
            {
                var result = await _repositoryWrapper.Account.Login(loginViewModel);

                if (result.StatusCode == Core.StatusCode.Unauthorized) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                //TODO add log
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("user/details")]
        public async Task<IActionResult> Details()
        {
            try
            {
                string userId = GetUserIdFromToken();

                var result = await _repositoryWrapper.Account.GetUserDetails(userId);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                //TODO add log
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel registrationViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Data that you entered is not valid!");
                }

                var result = await _repositoryWrapper.Account.Register(registrationViewModel);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                //TODO add log
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpGet("account")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                //return Redirect("http://localhost:5000/users"); test

                var result = await _repositoryWrapper.Account.VerifyUserEmail(userId, code);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);
                
                return Redirect($"http://localhost:5000/login?isSuccess={result.Success}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
