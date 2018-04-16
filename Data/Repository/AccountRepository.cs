using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using ViewModels;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Category>, IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public AccountRepository(AppDbContext appDbContext, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Login(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,
                loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //todo
            }
        }
    }
}
