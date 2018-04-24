using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using LimeBox.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace LimeBox.Models
{
    public class TestAccountRepository : IAccountRepository
    {
        public async Task<bool> AddNewUserAsync(AccountCreateVM.CreateFormVM model)
        {
            return true;
        }

        public Task AddRoleAsync(string name)
        {
            throw new NotImplementedException();
        }

        public AccountLoginVM GetLoginVM(IIdentity user)
        {
            throw new NotImplementedException();
        }

        public string GetReturnUrl(HttpRequest request)
        {
            return "/Test/test";
        }

        public async Task<bool> TryLoginAsync()
        {
            return true;
        }

        public async Task<bool> TryLoginAsync(AccountLoginVM viewModel)
        {
            return true;
        }

        public async Task TryLogOutAsync()
        {

        }

        public async Task UpdateUser(AccountSettingsVM.CreateFormVM createForm)
        {
            
        }

        public bool UserIsLoggedIn(ClaimsPrincipal user)
        {
            return true;
        }
    }
}
