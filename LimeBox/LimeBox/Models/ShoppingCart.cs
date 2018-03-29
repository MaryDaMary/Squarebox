using LimeBox.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeBox.Models
{
    public class ShoppingCart
    {
        static List<Boxes> cart = new List<Boxes>();

        static public void AddToCart(Boxes box)
        {
            cart.Add(box);
        }

        static public void RemoveFromCart(Boxes box)
        {
            cart.RemoveAll(b => b.Id == box.Id);
        }

        static public List<Boxes> GetCart()
        {
            return cart;
        }
    }
}
