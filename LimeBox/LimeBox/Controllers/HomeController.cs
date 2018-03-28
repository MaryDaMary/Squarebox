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


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Test()
        {
            var testList = repository.GenerateBoxes("LimeBox", 199).OrderBy(o => o.BoxValue).ToList();
            return View(new TestVM
            {
                boxes = testList
            });
        }
    }
}
