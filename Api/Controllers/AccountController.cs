using System;
using System.Threading.Tasks;
using Contracts;
using Core;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AccountController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpPost("api/login")]
        public async Task<OkResult> Login([FromBody] LoginViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repositoryWrapper.Account.Login(userViewModel);
                    return Ok();
                }

                return null;
            }
            catch (Exception e)
            {
                //add log
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] LoginViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repositoryWrapper.Account.Login(userViewModel);
                    return Ok();
                }

                return BadRequest("");

            }
            catch (Exception e)
            {
                //add log
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
