using LimeBox.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Views.Shared.Components.BoxScroll
{
    public class BoxScrollViewComponent : ViewComponent
    {
        private IRepository repository;

        public BoxScrollViewComponent(IRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.GetBoxesDataBase());
        }
    }
}
