using System.Threading.Tasks;
using Core;
using ViewModels;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task<ResponseObject<object>> Login(LoginViewModel loginViewModel);
        Task<ResponseObject<object>> GetUserDetails(string token);
        Task<ResponseObject<object>> Register(RegistrationViewModel registrationViewModel);
    }
}
