using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimeBox.Models;
using LimeBox.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LimeBox.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var foo = await accountRepository.TryLoginAsync();
            return View();
        }

        [HttpGet]
        public IActionResult Create(string returnUrl )
        {
            return View(new AccountCreateVM
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountCreateVM.CreateFormVM model)
        {
            await accountRepository.AddNewUserAsync(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
