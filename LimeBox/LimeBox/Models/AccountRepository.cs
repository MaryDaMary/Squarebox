using LimeBox.Models.Entities;
using LimeBox.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using static LimeBox.Models.ViewModels.AccountCreateVM;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace LimeBox.Models
{
    public class AccountRepository : Controller
    {
        LimeContext context;

        UserManager<IdentityUser> userManager;

        internal AccountLoginVM GetLoginVM(IIdentity user)
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

        public async Task<bool> TryLoginAsync()
        {
            var createSchemaResult = await identityDbContext.Database.EnsureCreatedAsync();

            return true;
        }

        public async Task TryLogOutAsync()
        {
            await signInManager.SignOutAsync();

        }

        public async Task AddNewUserAsync(CreateFormVM model)
        {
            //var newUser = new IdentityUser(model.UserName);
            var newUser = new IdentityUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };

            var createResult = await userManager.CreateAsync(newUser, model.PassWord);

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
        }

        public async Task<bool> TryLoginAsync(AccountLoginVM viewModel)
        {

            var loginResult = await signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            return loginResult.Succeeded;
        }

        //public async Task GetCurrentUser(HttpContext user)
        //{
        //    http = userManager.
        //    var id = http.Connection.Id;
        //    userManager.GetUserId(http.Connection.Id);
        //}
    }
}
