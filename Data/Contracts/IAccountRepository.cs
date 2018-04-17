using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Entities.Models;
using ViewModels;

namespace Contracts
{
    public interface IAccountRepository
    {
        Task<ResponseObject<object>> Login(LoginViewModel loginViewModel);
    }
}
