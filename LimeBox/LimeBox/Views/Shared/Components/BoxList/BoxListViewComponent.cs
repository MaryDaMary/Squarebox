using LimeBox.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Views.Shared.Components.BoxList
{
    public class BoxListViewComponent : ViewComponent
    {
        private Repository repository;

        public BoxListViewComponent(Repository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.GetBoxesNavBar());
        }
    }
}
