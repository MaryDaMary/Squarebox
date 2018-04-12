using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimeBox.Models;
using LimeBox.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LimeBox.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        Repository repository;

        public AdminController(Repository repository)
        {
            this.repository = repository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddBoxes()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBoxes(AdminAddBoxesVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            int nr = repository.CreateBoxType(model.BoxType, model.BoxImage, model.BoxImageHeader, model.BoxDescription);
            repository.GenerateBoxes(nr, (decimal)model.BoxPrice);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
