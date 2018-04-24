using LimeBox.Models.Entities;
using LimeBox.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LimeBox.Models
{
    public class AccountRepository : Controller, IAccountRepository
    {
        LimeContext context;

        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        IdentityDbContext identityDbContext;


        public AccountRepository(
            LimeContext context,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IdentityDbContext identityDbContext
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.identityDbContext = identityDbContext;
            this.context = context;
        }

        public AccountLoginVM GetLoginVM(IIdentity user)
        {
            var ret = new AccountLoginVM
            {
                IsLoggedIn = user.IsAuthenticated
            };

            if (user.IsAuthenticated)
            {
                ret.Username = user.Name;
            }
            else
            {

            }

            return ret;
        }

        public async Task AddRoleAsync(string name)
        {
            IdentityRole role = new IdentityRole();
            role.Name = name;
            await roleManager.CreateAsync(role);

        }

        public async Task<bool> TryLoginAsync()
        {

            var createSchemaResult = await identityDbContext.Database.EnsureCreatedAsync();

            return true;
        }

        public async Task TryLogOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<bool> AddNewUserAsync(AccountCreateVM.CreateFormVM model)
        {
            var newUser = new IdentityUser { UserName = model.Username, Email = model.Email, PhoneNumber = model.PhoneNumber };

            var createResult = await userManager.CreateAsync(newUser, model.Password);
            if (createResult.Succeeded)
            {
                context.Users.Add(new Users
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PostalCode = model.PostalCode,
                    City = model.City,
                    AspNetId = newUser.Id

                });
                await context.SaveChangesAsync();
                await userManager.AddToRoleAsync(newUser, "Standard");
                return true;
            }
            else return false;
        }

        public async Task<bool> TryLoginAsync(AccountLoginVM viewModel)
        {

            var loginResult = await signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            return loginResult.Succeeded;
        }

        public async Task UpdateUser(AccountSettingsVM.CreateFormVM createForm)
        {
            var aspUser = await userManager.FindByNameAsync(createForm.Username);
            aspUser.Email = createForm.Email;
            aspUser.PhoneNumber = createForm.PhoneNumber;
            await userManager.UpdateAsync(aspUser);

            if (createForm.OldPassword != null && createForm.NewPassword != null)
            {
                await userManager.ChangePasswordAsync(aspUser, createForm.OldPassword, createForm.NewPassword);
            }
            var user = context.Users.Single(u => u.AspNetId == aspUser.Id);
            user.FirstName = createForm.FirstName;
            user.LastName = createForm.LastName;
            user.Address = createForm.Address;
            user.City = createForm.City;
            user.PostalCode = createForm.PostalCode;
            context.SaveChanges();
        }

        public string GetReturnUrl(HttpRequest request)
        {
            return request.Headers["Referer"].ToString();
        }

        public bool UserIsLoggedIn(ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated;
        }
    }
}
