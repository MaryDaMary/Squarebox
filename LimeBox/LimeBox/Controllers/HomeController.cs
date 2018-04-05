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
    public class HomeController : Controller
    {
        Repository repository;

        public HomeController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var boxes = repository.GetBoxesDataBase();

            return View(boxes);
        }

        [HttpGet]
        public IActionResult ManyBoxes(int Id)
        {
            var boxes = repository.GetManyBoxesVM(Id);

            return View(boxes);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult ReadMore()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            return View(await repository.GetHomeCheckoutVM(User));
        }

        [HttpPost]
        public IActionResult CheckOut(HomeCheckoutVM model)
        {
            if (!ModelState.IsValid || ShoppingCart.IsEmpty() )
            {
                model.Boxes = ShoppingCart.GetCart();
                return View(model);
            }

            model.Boxes = ShoppingCart.GetCart();
            repository.CreateOrder(model, User);
            return RedirectToAction(nameof(Confirmation));
        }

        [HttpGet]
        public IActionResult Confirmation()
        {
            return View();
        }



    }
}
