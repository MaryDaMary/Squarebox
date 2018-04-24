using LimeBox.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Views.Shared.Components.BoxScroll
{
    public class CheckOutViewComponent : ViewComponent
    {
        private IAccountRepository repository;

        public CheckOutViewComponent(IAccountRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.GetLoginVM(User.Identity));
        }
    }
}
