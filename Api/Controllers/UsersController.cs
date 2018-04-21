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
        public Task<ResponseObject<object>> GetUsersList()
        {
            try
            {
                string userId = GetUserIdFromToken();

                var result = _repositoryWrapper.Users.GetUsersList(userId);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("users/add")]
        public Task<ResponseObject<object>> AddNewUser([FromBody] UsersViewModel userViewModel)
        {
            try
            {
                string userId = GetUserIdFromToken();
                
                var result = _repositoryWrapper.Users.SaveNewUserAsync(userId, userViewModel);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("users/update")]
        public Task<ResponseObject<object>> UpdateUser([FromBody] UsersViewModel userViewModel)
        {
            try
            {
                var result = _repositoryWrapper.Users.UpdateUserAsync(userViewModel);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpDelete("users/delete/{userId}")]
        public Task<ResponseObject<object>> DeleteUser(string userId)
        {
            try
            {
                var result = _repositoryWrapper.Users.DeleteUserAsync(userId);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("users/list/roles")]
        public ResponseObject<object> GetListOfRoles()
        {
            try
            {
                var result = _repositoryWrapper.Users.GetListOfRoles();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}