using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using LimeBox.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace LimeBox.Models
{
    public interface IAccountRepository
    {
        Task<bool> AddNewUserAsync(AccountCreateVM.CreateFormVM model);
        Task AddRoleAsync(string name);
        AccountLoginVM GetLoginVM(IIdentity user);
        Task<bool> TryLoginAsync();
        Task<bool> TryLoginAsync(AccountLoginVM viewModel);
        Task TryLogOutAsync();
        Task UpdateUser(AccountSettingsVM.CreateFormVM createForm);
        string GetReturnUrl(HttpRequest request);
        bool UserIsLoggedIn(ClaimsPrincipal user);
    }
}