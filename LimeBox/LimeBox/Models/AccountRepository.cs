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
    public class AccountRepository : Controller
    {
        LimeContext context;

        UserManager<IdentityUser> userManager;

        //public const string RoleName = "Admin";
        //public const string UserRole = "User";

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
            //var newUser = new IdentityUser(model.UserName);
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

        internal async Task UpdateUser(AccountSettingsVM.CreateFormVM createForm)
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

        //public async Task RoleType(CreateFormVM model)
        //{

        //    var result = await roleManager.CreateAsync(new IdentityRole(RoleName));
        //    if (result.Succeeded)
        //    {
        //        await userManager.AddToRoleAsync(User, RoleName);
        //    }
        //    string userId = userManager.GetUserId(HttpContext.newUser);
        //}

        //public async Task<bool> TryLoginAsync(CreateFormVM model)
        //{


        //    var createResult = await userManager.CreateAsync(new IdentityUser(model.UserName), model.PassWord);
        //    if (createResult.Succeeded)
        //    {
        //        await CreateRoleAsync(model.UserName);
        //    }
        //    return createResult.Succeeded;

        //}

        //internal async Task CreateRoleAsync(string userName)
        //{
        //    var user = await userManager.FindByNameAsync(userName);
        //    IdentityRole role = new IdentityRole();
        //    role.Name = UserRole;
        //    await roleManager.CreateAsync(role);
        //    await userManager.AddToRoleAsync(user, UserRole);
        //}

        //public async Task<string> CheckUserRoleByIdAsync(AccountLoginVM viewModel)
        //{
        //    IdentityUser model = await userManager.FindByNameAsync(viewModel.Username);
        //    var admin = await userManager.IsInRoleAsync(model, RoleName);
        //    var user = await userManager.IsInRoleAsync(model, UserRole);
        //    if (admin)
        //    {
        //        return "Admin";
        //    }
        //    else if (user)
        //    {
        //        return "User";
        //    }
        //    else
        //        return "No user found";
        //}


        //public async Task GetCurrentUser(HttpContext user)
        //{
        //    http = userManager.
        //    var id = http.Connection.Id;
        //    userManager.GetUserId(http.Connection.Id);
        //}


    }
}
