using System.Threading.Tasks;
using Core;
using ViewModels;

namespace Contracts
{
    public interface IUsersRepository
    {
        Task<ResponseObject<object>> GetUsersList(string token);
        Task<ResponseObject<object>> SaveNewUserAsync(string userId, UsersViewModel usersViewModel);
        Task<ResponseObject<object>> GetListOfRoles(string userId);
        Task<ResponseObject<object>> UpdateUserAsync(UsersViewModel userViewModel);
        Task<ResponseObject<object>> DeleteUserAsync(string userId);
    }
}
