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

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
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
        public IActionResult CheckOut()
        {
            var cart = ShoppingCart.GetCart();
            return View(new HomeCheckoutVM { Boxes = cart });
        }

        [HttpPost]
        public IActionResult CheckOut(HomeCheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                model.Boxes = ShoppingCart.GetCart();
                return View(model);
            }

            model.Boxes = ShoppingCart.GetCart();
            repository.CreateOrder(model);
            return RedirectToAction(nameof(Conformation));
        }

        [HttpGet]
        public IActionResult Conformation()
        {
            
            return Content("Tack för ditt köp");
        }
    }
}
