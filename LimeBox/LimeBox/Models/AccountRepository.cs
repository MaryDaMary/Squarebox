using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models
{
    public class AccountRepository
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        IdentityDbContext identityDbContext;

        public AccountRepository(
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
        }

        public async Task<bool> TryLoginAsync()
        {
            // Create DB schema (first time)
            var createSchemaResult = await identityDbContext.Database.EnsureCreatedAsync();

            // Create a hard coded user (first time)

            //var loginResult = await signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            //return loginResult.Succeeded;
            return true;
        }
    }
}
