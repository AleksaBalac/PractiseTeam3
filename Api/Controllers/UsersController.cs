using System;
using System.Threading.Tasks;
using Contracts;
using Core;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Api.Controllers
{
    [Route("api/")]
    public class UsersController : BaseController
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UsersController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet("users/list")]
        public async Task<IActionResult> GetUsersList()
        {
            try
            {
                string userId = GetUserIdFromToken();

                var result = await _repositoryWrapper.Users.GetUsersList(userId);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("users/add")]
        public async Task<IActionResult> AddNewUser([FromBody] UsersViewModel userViewModel)
        {
            try
            {
                string userId = GetUserIdFromToken();

                var result = await _repositoryWrapper.Users.SaveNewUserAsync(userId, userViewModel);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("users/update")]
        public async Task<IActionResult> UpdateUser([FromBody] UsersViewModel userViewModel)
        {
            try
            {
                var result = await _repositoryWrapper.Users.UpdateUserAsync(userViewModel);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpDelete("users/delete/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                var result = await _repositoryWrapper.Users.DeleteUserAsync(userId);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("users/list/roles")]
        public async Task<IActionResult> GetListOfRoles()
        {
            try
            {
                string userId = GetUserIdFromToken();

                var result = await _repositoryWrapper.Users.GetListOfRoles(userId);

                if (result.StatusCode == Core.StatusCode.BadRequest) return BadRequest(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}