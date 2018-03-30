using LimeBox.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Views.Shared.Components.BoxList
{
    public class BoxNavViewComponent : ViewComponent
    {
        private Repository repository;

        public BoxNavViewComponent(Repository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.GetBoxesDataBase());
        }
    }
}
