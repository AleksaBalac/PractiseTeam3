using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using ViewModels;

namespace Contracts
{
    public interface IUsersRepository
    {
        ResponseObject<object> GetUsersList(string token);
        Task<ResponseObject<object>> SaveNewUserAsync(string userId, UsersViewModel usersViewModel);
        ResponseObject<object> GetListOfRoles();
    }
}
