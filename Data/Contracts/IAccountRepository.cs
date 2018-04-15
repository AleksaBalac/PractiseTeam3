using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using ViewModels;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task Login(LoginViewModel loginViewModel);
    }
}
