using System;
using System.Collections.Generic;
using System.Linq;
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
        public ResponseObject<object> GetUsersList()
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