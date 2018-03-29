using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LimeBox.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LimeBox.Controllers
{
    public class CartController : Controller
    {
        Repository repository;

        public CartController(Repository repository)
        {
            this.repository = repository;
        }

        public void RemoveFromCart(int id)
        {
            var cart = repository.FindBoxById(id);
            ShoppingCart.RemoveFromCart(cart);
        }

        public void AddToCart(int id)
        {
            var cart = repository.FindBoxById(id);
            ShoppingCart.AddToCart(cart);
        }
    }
}
