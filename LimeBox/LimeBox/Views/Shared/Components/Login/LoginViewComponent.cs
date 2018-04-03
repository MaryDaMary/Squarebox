using LimeBox.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Views.Shared.Components.BoxScroll
{
    public class LoginViewComponent : ViewComponent
    {
        private AccountRepository repository;

        public LoginViewComponent(AccountRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.GetLoginVM(User.Identity));
        }
    }
}
